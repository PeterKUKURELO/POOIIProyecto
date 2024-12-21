using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class AuditoriaRepository : BaseRepository
    {

        public IEnumerable<Auditoria> GetAuditorias()
        {
            return Listar("usp_get_auditorias", dr => new Auditoria
            {
                IdAuditoria = Convert.ToInt32(dr["IdAuditoria"]),
                IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                Observaciones = Convert.ToString(dr["Observaciones"]),
                FechaAuditoria = Convert.ToDateTime(dr["FechaAuditoria"]),
                Resultado = Convert.ToString(dr["Resultado"]),
                EstandarCalidad = new EstandarCalidad
                {
                    IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                    Nombre = Convert.ToString(dr["NombreEstandar"]) // Nombre del estándar
                }
            });
        }

        public string InsertarAuditoria(Auditoria auditoria)
        {
            return Ejecutar("usp_insert_auditoria", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdEstandar", auditoria.IdEstandar);
                cmd.Parameters.AddWithValue("@Observaciones", auditoria.Observaciones);
                cmd.Parameters.AddWithValue("@FechaAuditoria", auditoria.FechaAuditoria);
                cmd.Parameters.AddWithValue("@Resultado", auditoria.Resultado);
            });
        }

        public string ActualizarAuditoria(Auditoria auditoria)
        {
            return Ejecutar("usp_update_auditoria", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdAuditoria", auditoria.IdAuditoria);
                cmd.Parameters.AddWithValue("@IdEstandar", auditoria.IdEstandar);
                cmd.Parameters.AddWithValue("@Observaciones", auditoria.Observaciones);
                cmd.Parameters.AddWithValue("@FechaAuditoria", auditoria.FechaAuditoria);
                cmd.Parameters.AddWithValue("@Resultado", auditoria.Resultado);
            });
        }

        public string EliminarAuditoria(int id)
        {
            return Ejecutar("usp_delete_auditoria", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdAuditoria", id);
            });
        }
    }
}