using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class EstandarCalidadRepository : BaseRepository
    {
        public IEnumerable<EstandarCalidad> GetEstandaresCalidad()
        {
            return Listar("usp_get_estandares_calidad", dr => new EstandarCalidad
            {
                IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                Nombre = Convert.ToString(dr["Nombre"]),
                Descripcion = Convert.ToString(dr["Descripcion"]),
                Estado = Convert.ToString(dr["Estado"]),
                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                FechaLimite = Convert.ToDateTime(dr["FechaLimite"])
            });
        }

        public string InsertarEstandarCalidad(EstandarCalidad estandar)
        {
            return Ejecutar("usp_insert_estandar_calidad", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nombre", estandar.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", estandar.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", estandar.Estado);
                cmd.Parameters.AddWithValue("@FechaCreacion", estandar.FechaCreacion);
                cmd.Parameters.AddWithValue("@FechaLimite", estandar.FechaLimite);
            });
        }

        public string ActualizarEstandarCalidad(EstandarCalidad estandar)
        {
            return Ejecutar("usp_update_estandar_calidad", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdEstandar", estandar.IdEstandar);
                cmd.Parameters.AddWithValue("@Nombre", estandar.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", estandar.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", estandar.Estado);
                cmd.Parameters.AddWithValue("@FechaCreacion", estandar.FechaCreacion);
                cmd.Parameters.AddWithValue("@FechaLimite", estandar.FechaLimite);
            });
        }

        public string EliminarEstandarCalidad(int id)
        {
            return Ejecutar("usp_delete_estandar_calidad", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdEstandar", id);
            });
        }
    }
}