using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Models;
using GestionAcademica.Repositories;

namespace GestionAcademica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository repo = new UsuarioRepository();
        private readonly RolRepository repoRol = new RolRepository();
        // GET: Usuario
        public ActionResult Index(int page = 1)
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            int pageSize = 10; // Número de elementos por página
            var usuarios = repo.GetUsuarios(); // Obtener todos los usuarios

            // Calcular la cantidad total de páginas
            int totalUsuarios = usuarios.Count();
            int totalPages = (int)Math.Ceiling((double)totalUsuarios / pageSize);

            // Obtener los elementos para la página actual
            var usuariosPaginados = usuarios
                .OrderBy(u => u.IdUsuario) // Ordenar por Id (o el criterio que prefieras)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pasar los datos a la vista
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(usuariosPaginados);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                // Buscar el usuario por ID
                var usuario = repo.GetUsuarios().FirstOrDefault(u => u.IdUsuario == id);
                if (usuario == null)
                {
                    TempData["Error"] = "Usuario no encontrado.";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            // Obtener los roles para el dropdown
            ViewBag.lstRoles = new SelectList(repoRol.GetRoles(), "IdRol", "Nombre");
            return View(new Usuario());
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección contra CSRF
        public ActionResult Create(Usuario usuario)
        {
            // Volver a llenar la lista de roles en caso de error
            ViewBag.lstRoles = new SelectList(repoRol.GetRoles(), "IdRol", "Nombre", usuario.IdRol);

            try
            {
                if (ModelState.IsValid) // Validar el modelo
                {
                    var mensaje = repo.InsertarUsuario(usuario); // Insertar el usuario (CodUsuario se genera automáticamente)
                    TempData["Mensaje"] = mensaje;
                    return RedirectToAction("Index");
                }
                else
                {
                    // Si el modelo no es válido, volver a la vista con los datos actuales
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al crear el usuario: {ex.Message}";
                return View(usuario);
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                // Buscar el usuario por ID
                var usuario = repo.GetUsuarios().FirstOrDefault(u => u.IdUsuario == id);
                if (usuario == null)
                {
                    TempData["Error"] = "Usuario no encontrado.";
                    return RedirectToAction("Index");
                }

                // Obtener la lista de roles y asignarla a ViewBag
                ViewBag.lstRoles = new SelectList(repoRol.GetRoles(), "IdRol", "Nombre", usuario.IdRol);

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Actualizar el usuario en la base de datos
                    var mensaje = repo.ActualizarUsuario(usuario);
                    TempData["Mensaje"] = mensaje;
                    return RedirectToAction("Index");
                }

                // Si el modelo no es válido, recargar la lista de roles
                ViewBag.lstRoles = new SelectList(repoRol.GetRoles(), "IdRol", "Nombre", usuario.IdRol);
                return View(usuario);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.lstRoles = new SelectList(repoRol.GetRoles(), "IdRol", "Nombre", usuario.IdRol);
                return View(usuario);
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var mensaje = repo.EliminarUsuario(id);
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        
    }
}
