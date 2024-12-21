using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class IndicadorDesempenoRepository : BaseRepository
    {
        public IEnumerable<IndicadorDesempeno> GetIndicadorDesempeno()
        {
            return Listar("usp_get_indicadores_desempeno", dr => new IndicadorDesempeno
            {
                IdIndicador = Convert.ToInt32(dr["IdIndicador"]),
                IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                Nombre = Convert.ToString(dr["Nombre"]),
                ResultadoActual = Convert.ToSingle(dr["ResultadoActual"]), 
                FechaActualizacion = Convert.ToDateTime(dr["FechaActualizacion"]),
                EstandarCalidad = new EstandarCalidad
                {
                    IdEstandar = Convert.ToInt32(dr["IdEstandar"]),
                    Nombre = Convert.ToString(dr["NombreEstandar"]) 
                }
            });
        }

        public string InsertarIndicadorDesempeno(IndicadorDesempeno indicador)
        {
            return Ejecutar("usp_insert_indicador_desempeno", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdEstandar", indicador.IdEstandar);
                cmd.Parameters.AddWithValue("@Nombre", indicador.Nombre);
                cmd.Parameters.AddWithValue("@ResultadoActual", indicador.ResultadoActual);
                cmd.Parameters.AddWithValue("@FechaActualizacion", indicador.FechaActualizacion);
            });
        }

        public string ActualizarIndicadorDesempeno(IndicadorDesempeno indicador)
        {
            return Ejecutar("usp_update_indicador_desempeno", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdIndicador", indicador.IdIndicador);
                cmd.Parameters.AddWithValue("@IdEstandar", indicador.IdEstandar);
                cmd.Parameters.AddWithValue("@Nombre", indicador.Nombre);
                cmd.Parameters.AddWithValue("@ResultadoActual", indicador.ResultadoActual);
                cmd.Parameters.AddWithValue("@FechaActualizacion", indicador.FechaActualizacion);
            });
        }

        public string EliminarIndicadorDesempeno(int id)
        {
            return Ejecutar("usp_delete_indicador_desempeno", cmd =>
            {
                cmd.Parameters.AddWithValue("@IdIndicador", id);
            });
        }
    }
}