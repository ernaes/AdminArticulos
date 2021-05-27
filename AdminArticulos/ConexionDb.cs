using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminArticulos
{
    public class ConexionDb
    {
        public static MySqlConnection ObtenerConexion()
        {
            string cs = @"server=localhost;userid=root;password=123456;database=prueba2";

            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            return con;
        
        }

        
    }
}
