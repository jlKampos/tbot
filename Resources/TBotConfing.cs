using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleToAttribute("TBotParsers")]

namespace Resources
{
	public class TBotConfing
	{
		private string botMaster;
		private long chatGroupID;
		private string tBotKEY;
		private string googleKEY;
		private string googleCX;

		public TBotConfing()
		{
			botMaster = string.Empty;
			chatGroupID = 0;
			tBotKEY = string.Empty;
			googleKEY = string.Empty;
			googleCX = string.Empty;
		}

		public string TelegramBotAPIKEY
		{
			get { return tBotKEY; }
		}

		public string GoogleAPIKEY
		{
			get { return googleKEY; }
		}


		public string GoogleAPICX
		{
			get { return googleCX; }
		}

		public string BotMaster
		{
			get { return botMaster; }
		}

		public long ChatGroupID
		{
			get { return chatGroupID; }
		}

		internal bool SetBotMaster(string bMaster)
		{
			if (bMaster != botMaster)
			{
				botMaster = bMaster;
				return true;
			}
			return false;
		}

		internal bool SetChatGroupID(long chatID)
		{
			if (chatID != chatGroupID)
			{
				chatGroupID = chatID;
				return true;
			}
			return false;
		}

		internal bool SetGoogleCX(string cx)
		{
			if (cx != googleCX)
			{
				googleCX = cx;
				return true;
			}
			return false;
		}

		internal bool SetGoogleKEY(string key)
		{
			if (key != googleKEY)
			{
				googleKEY = key;
				return true;
			}
			return false;
		}

		internal bool SetTelegramBotKEY(string key)
		{
			if (key != tBotKEY)
			{
				tBotKEY = key;
				return true;
			}
			return false;
		}
	}
}
