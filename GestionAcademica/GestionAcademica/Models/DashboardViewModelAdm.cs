using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAcademica.Models
{
    public class DashboardViewModelAdm
    {
        // Usar EstandarCalidad directamente para representar el estándar
        public EstandarCalidad Estandar { get; set; }

        // Usar Usuario para representar al responsable
        public Usuario Responsable { get; set; }

        // Usar IndicadorDesempeno para los indicadores relacionados
        public IndicadorDesempeno Indicador { get; set; }

        // Agregar propiedades adicionales según sea necesario
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaAuditoria { get; set; }
        public string ResultadoAuditoria { get; set; }
    }
}