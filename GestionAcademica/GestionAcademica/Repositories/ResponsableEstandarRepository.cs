using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class ResponsableEstandarRepository : BaseRepository
    {
        #region Listar Responsables de Estándares
        public IEnumerable<ResponsableEstandar> GetResponsablesEstandares()
        {
            return Listar("usp_get_responsables_estandar", dr => new ResponsableEstandar
            {
                IdRespEstandar = Convert.ToInt32(dr["IdRespEstandar"]),
                IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                EstandarCalidad = new EstandarCalidad
                {
                    IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                    Nombre = Convert.ToString(dr["NombreEstandar"])
                },
                Usuario = new Usuario
                {
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                    Nombre = Convert.ToString(dr["NombreUsuario"])
                }
            });
        }
        #endregion

        #region Insertar Responsable de Estándar
        public string InsertarResponsableEstandar(ResponsableEstandar responsable)
        {
            return Ejecutar("usp_insert_responsable_estandar", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdEstandar", responsable.IdEstandar);
                cmd.Parameters.AddWithValue("@IdUsuario", responsable.IdUsuario);
            });
        }
        #endregion

        #region Actualizar Responsable de Estándar
        public string ActualizarResponsableEstandar(ResponsableEstandar responsable)
        {
            return Ejecutar("usp_update_responsable_estandar", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdResponsableEstandar", responsable.IdRespEstandar);
                cmd.Parameters.AddWithValue("@IdEstandar", responsable.IdEstandar);
                cmd.Parameters.AddWithValue("@IdUsuario", responsable.IdUsuario);
            });
        }
        #endregion

        #region Eliminar Responsable de Estándar
        public string EliminarResponsableEstandar(int id)
        {
            return Ejecutar("usp_delete_responsable_estandar", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdResponsableEstandar", id);
            });
        }
        #endregion
    }
}