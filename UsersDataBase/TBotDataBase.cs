using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using Resources;
using TExceptions;

namespace UsersDataBase
{
	public class TBotDataBase : ITBotDataBase
	{
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

		public string GetDBName()
		{
			return myDB;
		}

		public void InitDB()
		{
			try
			{
				if (!File.Exists(myDB))
				{
					System.IO.Directory.CreateDirectory(Path.GetDirectoryName(myDB));
					CreateDB().Wait();
					return;
				}
				return;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<bool> InitDBAsync()
		{
			try
			{
				if (!File.Exists(myDB))
				{
					System.IO.Directory.CreateDirectory(Path.GetDirectoryName(myDB));
					return await CreateDB();
				}
				return false;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		async Task<bool> CreateDB()
		{
			try
			{
				SQLiteConnection.CreateFile(myDB);

				SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB));
				dbConn.Open();

				string sql = "create table tblTBotUser (ID INTEGER PRIMARY KEY, Username varchar(100) UNIQUE, IsAdmin int)";

				SQLiteCommand command = new SQLiteCommand(sql, dbConn);
				await command.ExecuteNonQueryAsync();

				dbConn.Close();
				return true;
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new ExceptionTBotDBCreatingDataBase(ex.Message);
			}
		}


		public int UsersCount()
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					string sqlCMD = "SELECT COUNT(ID) FROM tblTBotUser;";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					dbConn.Open();

					command = new SQLiteCommand(sqlCMD, dbConn);

					int amountUsers = Convert.ToInt32(command.ExecuteScalar());

					return amountUsers;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public List<TBotUser> GetUserList()
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					var userList = new List<TBotUser>();

					string sqlCMD = "select * from tblTBotUser;";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					dbConn.Open();

					command = new SQLiteCommand(sqlCMD, dbConn);
					command.ExecuteNonQueryAsync().Wait();
					var dr = command.ExecuteReader();

					while (dr.Read())
					{
						TBotUser user = new TBotUser();
						user.SetID((long)dr["ID"]);
						user.SetIsUsername((string)dr["Username"]);
						user.SetIsAdmin(Convert.ToBoolean((int)dr["IsAdmin"]));
						userList.Add(user);
					}
					dbConn.Close();

					return userList;
				}

			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<List<TBotUser>> GetUserListAsync()
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					var userList = new List<TBotUser>();

					string sqlCMD = "select * from tblTBotUser;";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					dbConn.Open();

					command = new SQLiteCommand(sqlCMD, dbConn);
					var dr = await command.ExecuteReaderAsync();

					while (dr.Read())
					{
						TBotUser user = new TBotUser();
						user.SetID((long)dr["ID"]);
						user.SetIsUsername((string)dr["Username"]);
						user.SetIsAdmin(Convert.ToBoolean((int)dr["IsAdmin"]));
						userList.Add(user);
					}
					dbConn.Close();

					return await Task.FromResult(userList);
				}

			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public bool InsertUser(string userName, bool isAdmin)
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					string sqlCMD = "insert into tblTBotUser (Username, IsAdmin) values (@Username, @IsAdmin)";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					command = new SQLiteCommand(sqlCMD, dbConn);
					command.ExecuteNonQueryAsync();

					dbConn.Open();
					command.Parameters.AddWithValue("Username", userName);
					command.Parameters.AddWithValue("IsAdmin", ConvertBoolToInt(isAdmin));
					Int32 rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected == 0)
					{
						dbConn.Close();
						throw new ExceptionTBotDBInsertingUser();
					}

					sqlCMD = "SELECT COUNT(ID) FROM tblTBotUser;";
					command = new SQLiteCommand(sqlCMD, dbConn);

					amountUsers = Convert.ToInt32(command.ExecuteScalar());
					dbConn.Clone();

					return true;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool RemoveUser(int id)
		{
			try
			{
				if (id == 1)
				{
					throw new ExceptionTBotDBRemovingAdmin();
				}

				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					string sqlCMD = "DELETE from tblTBotUser WHERE ID = @ID";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					command = new SQLiteCommand(sqlCMD, dbConn);
					command.ExecuteNonQueryAsync();

					dbConn.Open();
					command.Parameters.AddWithValue("ID", id);
					Int32 rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected == 0)
					{
						dbConn.Close();
						throw new ExceptionTBotDBRemovingUser();
					}

					sqlCMD = "SELECT COUNT(ID) FROM tblTBotUser;";
					command = new SQLiteCommand(sqlCMD, dbConn);

					amountUsers = Convert.ToInt32(command.ExecuteScalar());
					dbConn.Clone();

					return true;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool RemoveUser(string userName)
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{

					TBotUser user = GetUser(userName);

					if (user == null)
					{
						throw new ExceptionTBotDBUserNotfound();
					}

					if (user.ID == 1)
					{
						throw new ExceptionTBotDBRemovingAdmin();
					}

					string sqlCMD = "DELETE from tblTBotUser WHERE USERNAME = @Username";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					command = new SQLiteCommand(sqlCMD, dbConn);
					command.ExecuteNonQueryAsync();

					dbConn.Open();
					command.Parameters.AddWithValue("Username", userName);
					Int32 rowsAffected = command.ExecuteNonQuery();
					if (rowsAffected == 0)
					{
						dbConn.Close();
						throw new ExceptionTBotDBRemovingUser();
					}

					sqlCMD = "SELECT COUNT(ID) FROM tblTBotUser;";
					command = new SQLiteCommand(sqlCMD, dbConn);

					amountUsers = Convert.ToInt32(command.ExecuteScalar());
					dbConn.Clone();

					return true;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<bool> UpdateUser(int id, bool isAdmin)
		{
			try
			{
				if (id == 1)
				{
					return false;
				}

				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					string sqlCMD = "UPDATE tblTBotUser SET IsAdmin = @isAdmin WHERE ID = @ID;";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);

					dbConn.Open();
					command.Parameters.AddWithValue("isAdmin", ConvertBoolToInt(isAdmin));
					command.Parameters.AddWithValue("ID", id);
					Int32 rowsAffected = await command.ExecuteNonQueryAsync();
					dbConn.Close();

					if (rowsAffected == 0)
					{
						throw new ExceptionTBotDBUpdatingUser();
					}

					return true;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public TBotUser GetUser(int id)
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					TBotUser user = null;
					var userList = new List<TBotUser>();

					string sqlCMD = @"select * from tblTBotUser WHERE ID = @ID;";

					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);
					dbConn.Open();

					command.Parameters.Add(new SQLiteParameter("@ID") { Value = id });
					command.CommandType = System.Data.CommandType.Text;

					SQLiteDataReader dr;
					dr = command.ExecuteReader();

					while (dr.Read())
					{
						user = new TBotUser();
						user.SetID((long)dr["ID"]);
						user.SetIsUsername((string)dr["Username"]);
						user.SetIsAdmin(Convert.ToBoolean((int)dr["IsAdmin"]));
					}
					dbConn.Close();

					return user;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public TBotUser GetUser(string username)
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", myDB)))
				{
					TBotUser user = null;
					var userList = new List<TBotUser>();

					string sqlCMD = "select * from tblTBotUser WHERE Username = @Username;";
					SQLiteCommand command = new SQLiteCommand(sqlCMD, dbConn);
					dbConn.Open();

					command.Parameters.Add(new SQLiteParameter("@Username") { Value = username });
					command.CommandType = System.Data.CommandType.Text;

					SQLiteDataReader dr;
					dr = command.ExecuteReader();

					while (dr.Read())
					{
						user = new TBotUser();
						user.SetID((long)dr["ID"]);
						user.SetIsUsername((string)dr["Username"]);
						user.SetIsAdmin(Convert.ToBoolean((int)dr["IsAdmin"]));
					}
					dbConn.Close();

					return user;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		int ConvertBoolToInt(bool value)
		{
			if (value)
			{
				return 1;
			}
			return 0;
		}
	}
}
