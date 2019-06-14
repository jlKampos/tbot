
using Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UsersDataBase
{

	public interface ITBotDataBase
	{

		int UsersCount();

		string GetDBName();
		bool InsertUser(string userName, bool isAdmin);

		bool RemoveUser(int id);

		bool RemoveUser(string userName);

		Task<bool> UpdateUser(int id, bool isAdmin);

		Task<List<TBotUser>> GetUserListAsync();

		TBotUser GetUser(int id);

		TBotUser GetUser(string username);

	}
}
