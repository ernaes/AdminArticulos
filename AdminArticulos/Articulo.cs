using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminArticulos
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        //CREATE o UPDATE
        public void Guardar()
        {
            var con = ConexionDb.ObtenerConexion();
            string sql = "";
            if (this.Id > 0)
            {
                //Consulta de manipulacion de datos para un UPDATE
                sql = "update articulo set nombre=@nombre,categoria=@categoria,precio=@precio where id=@id";
            }
            else
            {
                //Consulta de manipulacion de datos para un INSERT
                sql = "insert into articulo (nombre,categoria,precio) values(@nombre,@categoria,@precio)";
            }
            
            using (MySqlCommand cmd=new MySqlCommand(sql,con) )

            {
                cmd.Parameters.AddWithValue("@nombre", this.Nombre);
                cmd.Parameters.AddWithValue("@categoria", this.Categoria);
                cmd.Parameters.AddWithValue("@precio", this.Precio);
                if (this.Id > 0)
                {
                    cmd.Parameters.AddWithValue("@id", this.Id);
                }
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        //READ
        public static Articulo Obtener(int articuloId)
        {
            Articulo r = null;
            var con = ConexionDb.ObtenerConexion();
            string sql = "select id,nombre,categoria,precio from articulo where id=@id";
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                
                cmd.Parameters.AddWithValue("@id", articuloId);                
                cmd.Prepare();
                using (MySqlDataReader dr=cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        r = new Articulo();
                        r.Id = articuloId;
                        r.Nombre = dr.GetString("nombre");
                        r.Categoria = dr.GetString("categoria");
                        r.Precio = dr.GetDecimal("precio");
                    }
                }
            }
            return r;
        }

        //DELETE
        public static void Eliminar(int articuloId)
        {
            var con = ConexionDb.ObtenerConexion();
            string sql = "delete from articulo where id=@id";
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                cmd.Parameters.AddWithValue("@id", articuloId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
