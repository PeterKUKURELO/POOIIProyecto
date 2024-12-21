using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAcademica.Models
{
    public class Auditoria
    {
        [Display(Name = "ID")]
        public int IdAuditoria { get; set; }
        [Display(Name = "ID Estándar")]
        public int IdEstandar { get; set; }
        public string Observaciones { get; set; }
        [Display(Name = "Fecha Auditoria")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAuditoria { get; set; }

        public string Resultado { get; set; }

        // Relación con Estándar de Calidad
        [Display(Name = "Estándar de Calidad")]
        public EstandarCalidad EstandarCalidad { get; set; }
    }
}