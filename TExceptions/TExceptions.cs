using Resources;
using System;

namespace TExceptions
{
	public class ExceptionGoogleParser : Exception
	{

		public ExceptionGoogleParser() : base(TBotExceptionStrings.ExceptionGoogleParserError)
		{
		}
		public ExceptionGoogleParser(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionGoogleParserError, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionGoogleResultItemsNULL : Exception
	{

		public ExceptionGoogleResultItemsNULL() : base(TBotExceptionStrings.ExceptionGoogleResultsNULL)
		{
		}
		public ExceptionGoogleResultItemsNULL(string message) : base(string.Format("{0}", string.Format(TBotExceptionStrings.ExceptionGoogleResultsNULL, message)))
		{
		}
	}

	public class ExceptionTbotSendMsgToTelegram : Exception
	{

		public ExceptionTbotSendMsgToTelegram() : base(TBotExceptionStrings.ExceptionTBotSendMSG)
		{
		}
		public ExceptionTbotSendMsgToTelegram(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionTBotSendMSG, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotChatBotReply : Exception
	{

		public ExceptionTBotChatBotReply() : base(TBotExceptionStrings.ExceptionTBotchatBotReply)
		{
		}
		public ExceptionTBotChatBotReply(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionTBotchatBotReply, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBCreatingDataBase : Exception
	{

		public ExceptionTBotDBCreatingDataBase() : base(TBotExceptionStrings.ExceptionDBCreatingDB)
		{
		}
		public ExceptionTBotDBCreatingDataBase(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBCreatingDB, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBUpdatingUser : Exception
	{

		public ExceptionTBotDBUpdatingUser() : base(TBotExceptionStrings.ExceptionDBUpdatingUser)
		{
		}
		public ExceptionTBotDBUpdatingUser(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBUpdatingUser, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBInsertingUser : Exception
	{
		public ExceptionTBotDBInsertingUser() : base(TBotExceptionStrings.ExceptionDBInsertingUser)
		{
		}
		public ExceptionTBotDBInsertingUser(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBInsertingUser, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBRemovingUser : Exception
	{
		public ExceptionTBotDBRemovingUser() : base(TBotExceptionStrings.ExceptionDBRemovingUser)
		{
		}
		public ExceptionTBotDBRemovingUser(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBRemovingUser, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBRemovingAdmin : Exception
	{
		public ExceptionTBotDBRemovingAdmin() : base(TBotExceptionStrings.ExceptionDBRemoveAdmin)
		{
		}
		public ExceptionTBotDBRemovingAdmin(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBRemoveAdmin, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotDBUserNotfound : Exception
	{
		public ExceptionTBotDBUserNotfound() : base(TBotExceptionStrings.ExceptionDBUserNotFound)
		{
		}
		public ExceptionTBotDBUserNotfound(string message) : base(string.Format("{0}\n{1}{2}", TBotExceptionStrings.ExceptionDBUserNotFound, TBotExceptionStrings.ExceptionOriginal, message))
		{
		}
	}

	public class ExceptionTBotConfigFailedParseChatID : Exception
	{
		public ExceptionTBotConfigFailedParseChatID() : base(TBotExceptionStrings.ExceptionTBotConfigFailedParseChatID)
		{
		}
		public ExceptionTBotConfigFailedParseChatID(string message) : base(string.Format("{0}", string.Format(TBotExceptionStrings.ExceptionTBotConfigFailedParseChatID, message)))
		{
		}
	}
}
