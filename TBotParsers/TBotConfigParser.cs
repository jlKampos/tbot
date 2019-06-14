using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TExceptions;

namespace TBotParsers
{
	public class TBotConfigParser
	{
		public TBotConfing ParseAPIConf(string jsonFile)
		{
			try
			{
				using (StreamReader r = new StreamReader(jsonFile))
				{
					string json = r.ReadToEnd();
					TBotConfing botConf = new TBotConfing();
					JObject jObject = JObject.Parse(json);
					JToken telegramBotAPI = jObject["TelegramBotAPI"];
					botConf.SetTelegramBotKEY((string)telegramBotAPI["KEY"]);
					JToken googleAPI = jObject["GoogleAPI"];
					botConf.SetGoogleCX((string)googleAPI["CX"]);
					botConf.SetGoogleKEY((string)googleAPI["KEY"]);
					botConf.SetBotMaster((string)jObject["Master"]);

					long chatID = 0;

					if (long.TryParse(jObject["ChatGroupID"].ToString(), out chatID))
					{
						botConf.SetChatGroupID(chatID);
					}

					return botConf;
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
	}
}
