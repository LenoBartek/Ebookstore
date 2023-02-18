using Dapper;
using Ebookstore.Interfaces;
using Ebookstore.Models;
using MySql.Data.MySqlClient;

namespace Ebookstore.Repositories
{
	public partial class UsersRepository : IUsersRepository
	{
		private readonly IConfiguration _config;
		private readonly string connectionString = "MysqlConnection";

		public UsersRepository(IConfiguration config)
		{
			_config = config;
		}


		//public int AddUser(UserModel userModel)
		//{
		//	string conn = _config.GetConnectionString(connectionString);
		//	using (var connection = new MySqlConnection(conn))
		//	{

		//		try
		//		{
		//			string sql = "INSERT INTO db_mysql.AspNetUsers(Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, BalanceRoleId, AccountBalance, FirstName, LastName, IsActive, Nid)" +
		//						"values(@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail,@EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount, @BalanceRoleId, @AccountBalance, @FirstName, @LastName, @IsActive, @Nid)";
		//			var affectedRows = connection.Execute(sql, userModel, commandType: System.Data.CommandType.Text);
		//			return (int)affectedRows;

		//		}
		//		catch (Exception ex)
		//		{
		//		}
		//	}
		//	return 0;
		//}
		

		//public int EditUser(UserModel userModel)
		//{
		//	if (userModel != null)
		//	{
		//		string conn = _config.GetConnectionString(connectionString);
		//		using (var connection = new MySqlConnection(conn))
		//		{
		//			try
		//			{
		//				String sUpdate = "Update db_mysql.Ebooks SET " +
		//					"Id = @Id, UserName= @UserName, NormalizedUserName = @NormalizedUserName, Email = @Email, NormalizedEmail = @NormalizedEmail, EmailConfirmed = @EmailConfirmed, PasswordHash = @PasswordHash, SecurityStamp = @SecurityStamp, ConcurrencyStamp = @ConcurrencyStamp, PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, " +
		//					"TwoFactorEnabled = @TwoFactorEnabled, LockoutEnd = @LockoutEnd, LockoutEnabled = @LockoutEnabled, AccessFailedCount = @AccessFailedCount, BalanceRoleId = @BalanceRoleId, AccountBalance = @AccountBalance, FirstName = @FirstName, LastName = @LastName, IsActive = @IsActive " +
		//					"where nid = @nid";
		//				var affectedRows = connection.Execute(sUpdate, userModel, commandType: System.Data.CommandType.Text);
		//				return (int)affectedRows;
		//			}
		//			catch (Exception ex)
		//			{

		//			}
		//		}
		//	}
		//	return 0;
		//}

		//public int DeleteUser(UserModel userModel)
		//{
		//	if (userModel != null)
		//	{
		//		string conn = _config.GetConnectionString(connectionString);
		//		using (var connection = new MySqlConnection(conn))
		//		{
		//			try
		//			{
		//				string sql = "Delete from db_mysql.AspNetUsers where nid =@nid";
		//				sql = sql + " and not exists (Select 1 from Shelf S where S.userid = AspNetUsers.nid)";  //not subscribed
		//				var affectedRows = connection.Execute(sql, userModel, commandType: System.Data.CommandType.Text);
		//				return (int)affectedRows;
		//			}
		//			catch (Exception ex)
		//			{

		//			}
		//		}
		//	}
		//	return 0;
		//}

		 //---

		public List<UserModel> UserList()
		{
			try
			{
				string conn = _config.GetConnectionString(connectionString);
				using (var connection = new MySqlConnection(conn))
				{
					connection.Open();
					string select = "SELECT id, name, surname, email, login, created_date, max_number_ebooks FROM Users";
					var tUsers = connection.Query<UserModel>(select, commandType: System.Data.CommandType.Text);
					return tUsers.ToList();
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}
}
