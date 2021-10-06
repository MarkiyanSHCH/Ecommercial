using Microsoft.Extensions.Configuration;

using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Repository
{
    public class DataBase
    {
        public DataTable ReadDatabase(IConfiguration _configuration, string query)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return table;
        }
    }
}
