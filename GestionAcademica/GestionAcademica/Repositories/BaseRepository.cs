using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionAcademica.Repositories
{
    public class BaseRepository
    {
        // Cadena de conexión reutilizable
        protected static readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        // Método genérico para listar registros
        protected IEnumerable<T> Listar<T>(string storedProcedure, Func<SqlDataReader, T> mapeoEntidad)
        {
            var lista = new List<T>();

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, cone))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(mapeoEntidad(dr));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar registros: {ex.Message}");
            }

            return lista;
        }

        // Método genérico para insertar y actualizar
        protected string Ejecutar(string storedProcedure, Action<SqlCommand> agregarParametros)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cone = new SqlConnection(cadena))
                {
                    cone.Open();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, cone))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        agregarParametros(cmd);

                        var respuesta = cmd.ExecuteNonQuery();
                        mensaje = respuesta > 0 ? "Operación realizada correctamente." : "No se pudo realizar la operación.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la operación: {ex.Message}");
            }

            return mensaje;
        }


    }
}