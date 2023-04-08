using System.Net.NetworkInformation;
using System.Data.SqlClient;
using HelloWorldWebApplication.Models;

namespace HelloWorldWebApplication.Services
{
    public class ProductService
    {
        private static string db_source = "az204db.database.windows.net";
        private static string db_user = "az204admin";
        private static string db_password = "SuperUser!";
        private static string db_database = "az204dbname";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> Products = new List<Product>();
            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using ( SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                   Products.Add(product);
                }
            }
            conn.Close();
            return Products;
         }
    }
}
