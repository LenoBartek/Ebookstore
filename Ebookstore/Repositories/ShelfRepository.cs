using Dapper;
using Ebookstore.Interfaces;
using Ebookstore.Models;
using MySql.Data.MySqlClient;


namespace Ebookstore.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly IConfiguration _config;
        private readonly string connectionString = "MysqlConnection";

        public ShelfRepository(IConfiguration config)
        {
            _config = config;
        }

        public int AddToShelf(ShelfModel shelfModel)
        {
            string conn = _config.GetConnectionString(connectionString);
            using (var connection = new MySqlConnection(conn))
            {
                try
                {
                    string sql = "INSERT INTO db_mysql.Shelf(id,ebook, userid, created_date)"
                    + " Select @id, @ebook, @userid, @created_date"
                    + " where not exists (Select 1 from db_mysql.Shelf S where S.ebook = @ebook)";

                    var affectedRows = connection.Execute(sql, shelfModel, commandType: System.Data.CommandType.Text);
                    return (int)affectedRows;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            return 0;
        }

        public int DeleteFromShelf(ShelfModel shelfModel)
        {
            if (shelfModel != null)
            {
                string conn = _config.GetConnectionString(connectionString);
                using (var connection = new MySqlConnection(conn))
                {
                    try
                    {
                        string sql = "Delete from db_mysql.Shelf where id =@id";
                        var affectedRows = connection.Execute(sql, shelfModel, commandType: System.Data.CommandType.Text);
                        return (int)affectedRows;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }

        public List<ShelfModel> ShelfList()
        {
            try
            {
                string conn = _config.GetConnectionString(connectionString);
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();
                    string select = "SELECT S.id, S.ebook, S.userid, S.created_date" +
                    ", E.authors, E.title, E.path, E.filename " +
                    " FROM Shelf S inner join Users U on U.id = S.userid" +
                    " left outer join Ebooks E on E.id = S.ebook" +
                    " where userid = U.id = 1";
                    
                    var tShelf = connection.Query<ShelfModel>(select, commandType: System.Data.CommandType.Text);
                    return tShelf.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
