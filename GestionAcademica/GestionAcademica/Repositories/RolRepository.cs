using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAcademica.Models;

namespace GestionAcademica.Repositories
{
    public class RolRepository : BaseRepository
    {
        public IEnumerable<Rol> GetRoles()
        {
            return Listar("usp_get_roles", dr => new Rol
            {
                IdRol = Convert.ToInt32(dr["IdRol"]),
                Nombre = Convert.ToString(dr["Nombre"])
            });
        }
    }
}