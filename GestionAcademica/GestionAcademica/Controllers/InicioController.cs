using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Models;
using GestionAcademica.Repositories;

namespace GestionAcademica.Controllers
{
    public class InicioController : Controller
    {
        private readonly UsuarioRepository repo = new UsuarioRepository();
        // GET: Inicio
        public ActionResult Index()
        {
            return View();
        }
        // POST:
        [HttpPost]
        public ActionResult Index(string usuario, string password)
        {
            // Llamar al método VerificarUsuario del DataBaseHelper
            var usuarioVerificado = repo.VerificarUsuario(usuario, password);

            if (usuarioVerificado != null)
            {
                // Si el usuario existe, guardar información en la sesión
                Session["IdUsuario"] = usuarioVerificado.IdUsuario;
                Session["NombreUsuario"] = usuarioVerificado.Nombre;
                Session["RolUsuario"] = usuarioVerificado.IdRol;

                // Redirigir según el rol del usuario
                switch (usuarioVerificado.IdRol)
                {
                    case 1: // Administrador
                        return RedirectToAction("Index", "IndexAdm");
                    case 2: // Docente
                        return RedirectToAction("Index", "IndexProf");
                    case 3: // Coordinador
                        return RedirectToAction("Index", "IndexCoordinador");
                    default:
                        // Si el rol no está definido, redirigir a una página de error o login
                        ViewBag.ErrorMessage = "Rol de usuario no reconocido.";
                        return View();
                }
            }
            else
            {
                // Si no se verifica, mostrar mensaje de error
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View();
            }
        }
    }
}