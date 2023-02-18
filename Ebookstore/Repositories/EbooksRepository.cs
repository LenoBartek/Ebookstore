using Dapper;
using Ebookstore.Interfaces;
using Ebookstore.Models;
using MySql.Data.MySqlClient;

namespace Ebookstore.Repositories
{
    public class EbooksRepository : IEbooksRepository
    {
        private readonly IConfiguration _config;
        private readonly string connectionString = "MysqlConnection";

        public EbooksRepository(IConfiguration config)
        {
            _config = config;
        }

        public int AddEbooks(EbooksModel ebooksModel)
        {
            string conn = _config.GetConnectionString(connectionString);
            using (var connection = new MySqlConnection(conn))
            {
                try
                {
                    string sql = "INSERT INTO db_mysql.Ebooks(id, authors, title, pages, path, filename, publication_year, added_date) " +
                                " Select @id, @authors, @title, @pages, @path, @filename, @publication_year, @added_date " +
                                " where not exists (Select 1 from db_mysql.Ebooks E where E.filename = @filename) ";


                    var affectedRows = connection.Execute(sql, ebooksModel, commandType: System.Data.CommandType.Text);
                    return (int)affectedRows;

                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public int EditEbooks(EbooksModel ebooksModel)
        {
            if (ebooksModel != null)
            {
                string conn = _config.GetConnectionString(connectionString);
                using (var connection = new MySqlConnection(conn))
                {
                    try
                    {
                        String sUpdate = "Update db_mysql.Ebooks SET authors = @authors, title = @title, pages = @pages, path = @path, filename = @filename, publication_year = @publication_year, added_date = @added_date where id = @id";
                        var affectedRows = connection.Execute(sUpdate, ebooksModel, commandType: System.Data.CommandType.Text);
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

        public int DeleteEbooks(EbooksModel ebooksModel)
        {
            if (ebooksModel != null)
            {
                string conn = _config.GetConnectionString(connectionString);
                using (var connection = new MySqlConnection(conn))
                {
                    try
                    {
                        string sql = "Delete from db_mysql.Ebooks where id =@id" +
                        " and not exists (Select 1 from Shelf S where S.ebook = Ebooks.id)";  //not subscribed
                        var affectedRows = connection.Execute(sql, ebooksModel, commandType: System.Data.CommandType.Text);
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

        public List<EbooksModel>? EbooksList(EbooksFilter filter)
        {
            try
            {
                string conn = _config.GetConnectionString(connectionString);
                using (var connection = new MySqlConnection(conn))
                {
                    connection.Open();
                    string select = "SELECT id, authors, title, pages, path, filename, publication_year, added_date FROM Ebooks ";

                    var parameters = new DynamicParameters();
                    if (filter != null)
                    {
                        if (filter.authors != "")
                        {
                            select = andWhere(select) + " authors like @authors";
                            parameters.Add("@authors", "%" + filter.authors + "%");
                        }
                        if (filter.title != "")
                        {
                            select = andWhere(select) + " title like @title";
                            parameters.Add("@title", "%" + filter.title + "%");
                        }
                        if (filter.publication_year > 0)
                        {
                            select = andWhere(select) + " CONVERT(publication_year, CHAR) like @publication_year";
                            parameters.Add("@publication_year", "%" + filter.publication_year.ToString() + "%");
                        }
                    }

                    var tEbooks = connection.Query<EbooksModel>(select, parameters, commandType: System.Data.CommandType.Text);
                    return tEbooks.ToList();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string andWhere(string s)
        {
            if (s.Contains("WHERE"))
                s = s + " AND ";
            else
                s = s + " WHERE ";
            return s;
        }

    }
}
