using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAcademica.Models
{
    public class ResponsableEstandar
    {
        [Display(Name = "Id Responsable ")]
        public int IdRespEstandar { get; set; }
        [Display(Name = "Id Estandar")]
        public int IdEstandar { get; set; }
        [Display(Name = "Id Usuario")]
        public int IdUsuario { get; set; }

        // Relaciones
        [Display(Name = "Estandar de Calidad")]
        public EstandarCalidad EstandarCalidad { get; set; }
        [Display(Name = "Nombre Usuario")]
        public Usuario Usuario { get; set; }
    }
}