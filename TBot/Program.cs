using System;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Resources;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Collections;
using Google.Apis.Services;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ChatterBotAPI;
using TExceptions;
using TBotParsers;
using UsersDataBase;
using System.Collections.Generic;
using System.Text;

namespace TBot
{
	class Program
	{
		static string botNick;
		static Match eval;
		static TBotDataBase db;
		static List<TBotUser> listUsers;
		static bool chatBotEnabled = false;
		static ChatterBotFactory factory = new ChatterBotFactory();
		static ChatterBot chatBot;
		static ChatterBotSession chatBotSession;
		private static TelegramBotClient tBot;
		static TBotConfing botConfig;

		static void Main(string[] args)
		{
			try
			{

				botConfig = new TBotConfigParser().ParseAPIConf("TBotConf.json");

				db = new TBotDataBase();
				db.InitDBAsync().Wait();

				if (db.UsersCount() == 0)
				{
					Console.WriteLine(TBotStrings.TBotNoUsersOnDB);
					db.InsertUser(botConfig.BotMaster, true);
				}
				else
				{
					TBotUser admin = db.GetUser(1);
					if (!admin.Username.Equals(botConfig.BotMaster))
					{
						Console.WriteLine(string.Format(TBotStrings.TBotNotThisBotAdmin, db.GetDBName()));
					}
				}

				listUsers = db.GetUserListAsync().Result;

				chatBot = factory.Create(ChatterBotType.PANDORABOTS, "f5d922d97e345aa1");

				chatBotSession = chatBot.CreateSession();

				tBot = new TelegramBotClient(botConfig.TelegramBotAPIKEY);

				var me = tBot.GetMeAsync().Result;
				botNick = me.FirstName;
				Console.Title = botNick;
				tBot.OnMessage += TBot_OnMessage;

				tBot.StartReceiving(Array.Empty<UpdateType>());

				if (botConfig.ChatGroupID != 0)
				{
					Task<bool> callTask = Task.Run(() => SendMsgToTelegram(botConfig.ChatGroupID, string.Format(TBotStrings.TBotGreetingMSG, me.FirstName)));

					callTask.Wait();
					bool greetingSent = callTask.Result;

					if (greetingSent)
					{
						Console.WriteLine("Greetings sent");
					}
				}

				Console.WriteLine($"I am user {me.Id} and my Username is {me.Username}.\nStart listening for @{me.FirstName}");
				Console.ReadLine();
				tBot.StopReceiving();

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static async void TBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs messageEventArgs)
		{
			var message = messageEventArgs.Message;
			try
			{
				if (message == null || message.Type != MessageType.Text) return;

				Console.WriteLine("Chat ID: " + message.Chat.Id);
				Console.WriteLine(string.Format("Message from {0} {1} in chat {2}\nMessagee is: {3}", message.From.Username, message.From.Id, message.Chat.Title, message.Text));


				if (chatBotEnabled)
				{
					bool isNormalchat = true;

					if (message.Text.StartsWith("!"))
					{
						isNormalchat = false;
					}

					if (isNormalchat)
					{
						try
						{
							if (message.Text.ToLower().Contains(botNick.ToLower()))
							{
								message.Text = Regex.Replace(message.Text.ToLower(), botNick.ToLower(), "ALICE", RegexOptions.IgnoreCase);
							}

							string botReply = chatBotSession.Think(message.Text);

							// lets mask the bot name shall we
							if (botReply.Contains("ALICE"))
							{
								botReply = Regex.Replace(botReply, "ALICE", botNick, RegexOptions.IgnoreCase);
							}

							await SendMsgToTelegram(message.Chat.Id, botReply);
							Console.WriteLine("chatBotReply to {0}: is {1}", message.Chat.Id, botReply);
						}
						catch (Exception ex)
						{
							Console.WriteLine(new ExceptionTBotChatBotReply(ex.Message));
							await SendMsgToTelegram(message.Chat.Id, (new ExceptionTBotChatBotReply(ex.Message)).ToString());
						}

					}
				}

				if (Regex.Match(message.Text.ToLower(), "!" + TBotCommand.HELP.ToString().ToLower()).Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					ResourceSet helpResources = HelpResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

					StringBuilder helpMsg = new StringBuilder();

					foreach (DictionaryEntry entry in helpResources)
					{
						helpMsg.AppendLine(string.Format(entry.Value.ToString() + Environment.NewLine));
					}
					await SendMsgToTelegram(message.Chat.Id, helpMsg.ToString());

				}
				else if (Regex.Match(message.Text.ToLower(), "!" + TBotCommand.CHATBOT.ToString().ToLower()).Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					chatBotEnabled = !chatBotEnabled;
					if (chatBotEnabled)
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.EnabledChatBot);
					}
					else
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.DisabledChatbot);
					}

				}
				else if ((eval = Regex.Match(message.Text, "!" + TBotCommand.GOOGLE.ToString().ToLower() + "\\s{1,}(.{1,100})", RegexOptions.IgnoreCase)) != null && eval.Success)
				{
					string query = eval.Groups[1].ToString(); // se nos derem ping, dizemos isso e feio 
					GoogleSearchResults result = await GoogleSearch(query);

					await SendMsgToTelegram(message.Chat.Id, "You have searched for: " + query);

					if (result != null)
					{
						await SendMsgToTelegram(message.Chat.Id, "Search time was: " + result.SearchTime);
						await SendMsgToTelegram(message.Chat.Id, "Total items found: " + result.TotalResults);

						if (result.Results.Count > 0)
						{
							foreach (var item in result.Results)
							{
								await SendMsgToTelegram(message.Chat.Id, "Title: " + item.Title);
								await SendMsgToTelegram(message.Chat.Id, "Link: " + item.Link);
							}
						}
						else if (result.Results.Count == 0 && result.TotalResults > 0)
						{
							await SendMsgToTelegram(message.Chat.Id, string.Format("Something not right, we have {0} search hits, but the list of results is empty", result.TotalResults));
						}

					}
				}
				else if ((eval = Regex.Match(message.Text, "!" + TBotCommand.SQLGETUSERS.ToString().ToLower(), RegexOptions.IgnoreCase)) != null && eval.Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					listUsers = db.GetUserList();

					foreach (TBotUser user in listUsers)
					{
						await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotUsersChatMSG, user.ID, Environment.NewLine, user.Username, Environment.NewLine, user.IsAdmin.ToString()));
					}
				}
				else if ((eval = Regex.Match(message.Text, "!" + TBotCommand.SQLINSERTUSER.ToString().ToLower() + "\\s+(.*)\\s+(.*)", RegexOptions.IgnoreCase)) != null && eval.Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					string user = string.Empty;
					bool isAdmin = false;

					if (!string.IsNullOrEmpty(eval.Groups[1].ToString()))
					{
						user = eval.Groups[1].ToString();
					}
					else
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.TBotNewUserParameterMising);
						return;
					}

					if (!string.IsNullOrEmpty(eval.Groups[2].ToString()))
					{
						if (eval.Groups[2].ToString().ToLower().Equals("true"))
						{
							isAdmin = true;
						}

						await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotNewUserCreted, db.InsertUser(user, isAdmin)));
					}
					else
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.TBotNewUserParameterMising);
						return;
					}

					listUsers = db.GetUserList();
				}
				else if ((eval = Regex.Match(message.Text, "!" + TBotCommand.SQLREMOVEUSER.ToString().ToLower() + "\\s+(.*)", RegexOptions.IgnoreCase)) != null && eval.Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					if (!string.IsNullOrEmpty(eval.Groups[1].ToString()))
					{
						int id = 0;

						if (int.TryParse(eval.Groups[1].ToString(), out id))
						{
							await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotUserIDRemoved, id, db.RemoveUser(id).ToString()));
						}
						else
						{
							string user = eval.Groups[1].ToString();
							await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotUserNameRemoved, user, db.RemoveUser(user).ToString()));
						}


					}
					else
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.TBotCMDArgumentMissing);
						return;
					}

					listUsers = db.GetUserList();
				}
				else if ((eval = Regex.Match(message.Text, "!" + TBotCommand.SQLUPDATEUSER.ToString().ToLower() + "\\s+(.*)\\s+(.*)", RegexOptions.IgnoreCase)) != null && eval.Success && listUsers.Exists(x => x.Username == message.From.Username && x.IsAdmin))
				{
					if (!string.IsNullOrEmpty(eval.Groups[1].ToString()) && !string.IsNullOrEmpty(eval.Groups[2].ToString()))
					{
						int id = 0;

						if (int.TryParse(eval.Groups[1].ToString(), out id))
						{
							bool isAdmin = false;

							if (bool.TryParse(eval.Groups[2].ToString(), out isAdmin))
							{
								await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotUserUpdated, db.UpdateUser(id, isAdmin).Result));
							}
							else
							{
								await SendMsgToTelegram(message.Chat.Id, string.Format(TBotStrings.TBotUserUpdated, false));

							}

						}

					}
					else
					{
						await SendMsgToTelegram(message.Chat.Id, TBotStrings.TBotCMDWrongArg);
						return;
					}

					listUsers = db.GetUserList();
				}

			}
			catch (ExceptionGoogleResultItemsNULL ex)
			{
				Console.WriteLine(ex.Message);
				await SendMsgToTelegram(message.Chat.Id, ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				await SendMsgToTelegram(message.Chat.Id, ex.Message);
			}

		}

		private static Task<GoogleSearchResults> GoogleSearch(string query)
		{
			try
			{
				WebClient webClient = new WebClient();
				webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134");

				var results = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&alt=json", botConfig.GoogleAPIKEY, botConfig.GoogleAPICX, query));

				JObject json = JObject.Parse(results);

				GoogleSearchResults googleResults = new GoogleJsonParser().ConvertJsonToGoogleResults(json);
				return Task.FromResult(googleResults);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static async Task<bool> SendMsgToTelegram(long chatID, string message)
		{
			Message msg;
			try
			{
				msg = await tBot.SendTextMessageAsync(chatID, message);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(new ExceptionTbotSendMsgToTelegram("chat id: " + chatID + "\n" + ex.Message));
				return false;
			}
		}
	}
}
