using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAcademica.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string CodUsuario { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }

        // Relacion con Rol 
        public Rol Rol { get; set; }
    }
}