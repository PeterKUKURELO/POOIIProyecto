using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class UsuarioRepository : BaseRepository
    {
        // Método para verificar credenciales de usuario
        public Usuario VerificarUsuario(string codUsuario, string password)
        {
            Usuario usuarioVerificado = null;
            string sp = "usp_VerificarUsuario";

            try
            {
                using (SqlConnection Cone = new SqlConnection(cadena))
                {
                    Cone.Open();
                    using (SqlCommand cmd = new SqlCommand(sp, Cone))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodUsuario", codUsuario);
                        cmd.Parameters.AddWithValue("@Password", password);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            usuarioVerificado = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = Convert.ToString(dr["Nombre"]),
                                IdRol = Convert.ToInt32(dr["IdRol"])
                            };
                        }

                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verificando usuario: {ex.Message}");
            }

            return usuarioVerificado;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return Listar("usp_get_usuarios", dr => new Usuario
            {
                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                Nombre = Convert.ToString(dr["Nombre"]),
                CodUsuario = Convert.ToString(dr["CodUsuario"]),
                Password = Convert.ToString(dr["Password"]),
                IdRol = Convert.ToInt32(dr["IdRol"]),
                Rol = new Rol
                {
                    Nombre = Convert.ToString(dr["NombreRol"])
                }
            });
        }
        public string InsertarUsuario(Usuario usuario)
        {
            return Ejecutar("usp_insert_usuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
            });
        }

        public string ActualizarUsuario(Usuario usuario)
        {
            return Ejecutar("usp_update_usuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
            });
        }
        public string EliminarUsuario(int id)
        {
            return Ejecutar("usp_delete_usuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdUsuario", id);
            });
        }
    }
}