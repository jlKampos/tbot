# tbot
## DOTNET Self contained Telegram BOT API Implementation 

This is a personal project of systems integration that implementes Public APIS, TelegramBot, GoogleAPI & PandoraBots...


**NOTE:** Pandorabots is called trough an API that is hosted at https://github.com/pierredavidbelanger/chatter-bot-api
there is a DLL provided in this repo located at:
\tbot\ChatterBotAPI\ChatterBotAPI.dll , you can always remove this dll, and add your own pre compiled dll.

All features were made for fun.
In time more commands will be added like weather lookups etc...

############################################################################################
#### **USAGE:**

**Edit:** TBotConf.json located at the TBot/ Project 
```
{
	"Master": "YOURTELEGRAM_USERNAME",
	"ChatGroupID": "-99999999", //the chatroom id, note you need to create a chatgroup and add the bot to the group
	"TelegramBotAPI": {
		"KEY": "0********1:BE**************************dhYf" // your telegramBot api key
	},
	"GoogleAPI": {
		"KEY": "BJ*************************XvN", // your google API KEY
		"CX": "04*************21:bab" // your goole CX KEY
	}
}
```

**NOTE:** this bot uses an SQLITE database to store users:
sqlite database name and path :P

```csharp
	int amountUsers;
		DataTable dt;
		string myDBName = "TBot.db";
		string myExecPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		string myDB;

		public TBotDataBase()
		{
			try
			{
				amountUsers = 0;
				dt = new DataTable();
				myDB = Path.Combine(myExecPath, this.GetType().Name, myDBName);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
```

**Users Commands:**

```
!help
!google searchTerm
    eg: !google sausages
!sqlgetusers
!sqlinsertuser USERNAME true|false(isadmin)
    eg: !sqlinsertuser root true
 !sqlremoveuser USERNAME|USERID
    eg: !sqlremoveuser root, !sqlremoveuser 2
 !sqlupdateuser USERNAME|USERID true|false(isadmin)
    eg: the same as the others 
!chatbot
    This will enable the chatBotAPI that will pull conversations from Pandorabots ALICE bot
    thet botname is replaced within the code to the bot.username
```

 **Since this is a self contained dotnet app, you can run it on mac ubuntu and windows, just publish the project
 to run execute:**
 
 dotnet run TBot.dll
 
 
 Any thing else let me known
 
 


