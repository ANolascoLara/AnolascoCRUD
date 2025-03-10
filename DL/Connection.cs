using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Connection
    {
        public static string GetConnection()
        {
            string conexion = ConfigurationManager.ConnectionStrings["ANolascoProgramacionNCapas"].ToString();
            return conexion;
        }

    }
}
