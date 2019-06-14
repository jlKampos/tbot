using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleToAttribute("TBotDB")]

namespace Resources
{
	public class TBotUser
	{
		long id;
		string userName;
		bool isAdmin;

		public TBotUser()
		{
			userName = "TBOT_DEFAULT_USERNAME";
			isAdmin = false;
		}

		public TBotUser(string uName, bool isAdmin)
		{
			this.userName = uName;
			this.isAdmin = isAdmin;
		}

		public long ID
		{
			get { return id; }
		}

		public string Username
		{
			get { return userName; }
		}


		public bool IsAdmin
		{
			get { return isAdmin; }
		}

		internal bool SetID(long id)
		{
			if (id != this.id)
			{
				this.id = id;
				return true;
			}

			return false;
		}

		internal bool SetIsUsername(string userName)
		{
			if (userName != this.userName)
			{
				this.userName = userName;
				return true;
			}

			return false;
		}

		internal bool SetIsAdmin(bool isAdmin)
		{
			if (isAdmin != this.isAdmin)
			{
				this.isAdmin = isAdmin;
				return true;
			}

			return false;
		}
	}
}
