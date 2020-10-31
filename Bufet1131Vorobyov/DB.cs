using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bufet1131Vorobyov
{
    public class DB
    {
        internal MySqlConnection connection = null;

        public DB()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            
            sb.Server = "localhost";
            sb.UserID = "root";

            /*
            sb.Server = "192.168.200.13";
            sb.UserID = "student";
            */
            sb.Database = "bufet1131vorobyov";
            sb.Password = "student";
            sb.CharacterSet = "UTF8";
            connection = new MySqlConnection(sb.ToString());
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
    }
}
