using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Repositories;

namespace GestionAcademica.Controllers
{
    public class IndexAdmController : Controller
    {
        private readonly DashboardRepository repo = new DashboardRepository();
        // GET: IndexAdm
        public ActionResult Index()
        {
            // Obtener el nombre del usuario desde la sesión
            string nombreUsuario = Session["NombreUsuario"]?.ToString() ?? "Invitado";

            // Pasar el nombre del usuario a la vista
            ViewBag.NombreUsuario = nombreUsuario;

            // Obtener datos del dashboard desde el repositorio
            var dashboardData = repo.GetDashboardData();

            // Pasar los datos del dashboard a la vista
            return View(dashboardData);
        }
    }
}