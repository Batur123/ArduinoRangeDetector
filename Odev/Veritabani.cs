using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev
{
    public class Veritabani
    {


        public static string GlobalServerName = "BATUR" + "\\" + "BATUR";

        public static SqlConnection GetConnection()
        {



            string connectionString = @"server=" + GlobalServerName + "; Initial Catalog=mesafe;Integrated Security=SSPI";

            return new SqlConnection(connectionString);
        }


    }
}
