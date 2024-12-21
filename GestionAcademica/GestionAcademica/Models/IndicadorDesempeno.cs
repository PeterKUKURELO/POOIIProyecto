using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAcademica.Models
{
    public class IndicadorDesempeno
    {
        [Display(Name = "ID")]
        public int IdIndicador { get; set; }

        [Display(Name = "ID Estándar")]
        public int IdEstandar { get; set; }

        [Display(Name = "Nombre del Indicador")]
        public string Nombre { get; set; }

        [Display(Name = "Resultado Actual")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // Formato con 2 decimales
        public float ResultadoActual { get; set; }

        [Display(Name = "Última Actualización")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaActualizacion { get; set; }

        // Relación con Estándar de Calidad
        [Display(Name = "Estándar de Calidad")]
        public EstandarCalidad EstandarCalidad { get; set; }
    }
}