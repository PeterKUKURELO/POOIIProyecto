using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class DashboardRepository : BaseRepository
    {
        public IEnumerable<DashboardViewModelAdm> GetDashboardData()
        {
            return Listar("usp_get_dashboard_data", dr => new DashboardViewModelAdm
            {
                Estandar = new EstandarCalidad
                {
                    IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                    Nombre = Convert.ToString(dr["NombreEstandar"]),
                    Descripcion = Convert.ToString(dr["DescripcionEstandar"])
                },
                Responsable = new Usuario
                {
                    IdUsuario = dr["IdUsuario"] != DBNull.Value ? Convert.ToInt32(dr["IdUsuario"]) : 0,
                    Nombre = dr["Responsable"] != DBNull.Value ? Convert.ToString(dr["Responsable"]) : "No asignado"
                },
                Indicador = new IndicadorDesempeno
                {
                    IdIndicador = dr["IdIndicador"] != DBNull.Value ? Convert.ToInt32(dr["IdIndicador"]) : 0,
                    Nombre = dr["Indicador"] != DBNull.Value ? Convert.ToString(dr["Indicador"]) : "No disponible",
                    ResultadoActual = dr["ResultadoActual"] != DBNull.Value ? Convert.ToSingle(dr["ResultadoActual"]) : 0
                },
                FechaAuditoria = dr["FechaAuditoria"] != DBNull.Value ? Convert.ToDateTime(dr["FechaAuditoria"]) : (DateTime?)null,
                ResultadoAuditoria = dr["ResultadoAuditoria"] != DBNull.Value ? Convert.ToString(dr["ResultadoAuditoria"]) : "No disponible"
            });
        }

    }
}