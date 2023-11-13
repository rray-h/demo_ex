using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace demo_ex
{
    internal class db_connect_class
    {
        SqlConnection sqlconnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");


        public void openConnection()
        {
            if(sqlconnection.State == System.Data.ConnectionState.Closed)
            {
                sqlconnection.Open();
            }

        }

        public void closeConnection()
        {
            if(sqlconnection.State == System.Data.ConnectionState.Open)
            {
                sqlconnection.Open();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlconnection;
        }
    }
}
