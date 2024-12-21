using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Models;
using GestionAcademica.Repositories;

namespace GestionAcademica.Controllers
{
    public class ResponsableEstandarController : Controller
    {
        // Instancias de repositorios
        private readonly ResponsableEstandarRepository repo = new ResponsableEstandarRepository();
        private readonly EstandarCalidadRepository estandarRepo = new EstandarCalidadRepository();
        private readonly UsuarioRepository usuarioRepo = new UsuarioRepository();

        #region Index
        // GET: ResponsableEstandar
        public ActionResult Index()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            var responsables = repo.GetResponsablesEstandares();
            return View(responsables);
        }
        #endregion

        #region Details
        // GET: ResponsableEstandar/Details/5
        public ActionResult Details(int id)
        {
            var responsable = repo.GetResponsablesEstandares()
                                  .FirstOrDefault(r => r.IdRespEstandar == id);
            if (responsable == null)
            {
                return HttpNotFound();
            }
            return View(responsable);
        }
        #endregion

        #region Create
        // GET: ResponsableEstandar/Create
        public ActionResult Create()
        {
            ViewBag.lstEstandares = new SelectList(estandarRepo.GetEstandaresCalidad(), "IdEstandar", "Nombre");
            ViewBag.lstUsuarios = new SelectList(usuarioRepo.GetUsuarios(), "IdUsuario", "Nombre");
            return View(new ResponsableEstandar());
        }

        // POST: ResponsableEstandar/Create
        [HttpPost]
        public ActionResult Create(ResponsableEstandar responsable)
        {
            ViewBag.lstEstandares = new SelectList(estandarRepo.GetEstandaresCalidad(), "IdEstandar", "Nombre", responsable.IdEstandar);
            ViewBag.lstUsuarios = new SelectList(usuarioRepo.GetUsuarios(), "IdUsuario", "Nombre", responsable.IdUsuario);

            try
            {
                var mensaje = repo.InsertarResponsableEstandar(responsable);
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(responsable);
            }
        }
        #endregion

        #region Edit
        // GET: ResponsableEstandar/Edit/5
        public ActionResult Edit(int id)
        {
            var responsable = BuscarResponsable(id);
            if (responsable == null)
            {
                return HttpNotFound();
            }

            ViewBag.lstEstandares = new SelectList(estandarRepo.GetEstandaresCalidad(), "IdEstandar", "Nombre", responsable.IdEstandar);
            ViewBag.lstUsuarios = new SelectList(usuarioRepo.GetUsuarios(), "IdUsuario", "Nombre", responsable.IdUsuario);
            return View(responsable);
        }

        // POST: ResponsableEstandar/Edit/5
        [HttpPost]
        public ActionResult Edit(ResponsableEstandar responsable)
        {
            ViewBag.lstEstandares = new SelectList(estandarRepo.GetEstandaresCalidad(), "IdEstandar", "Nombre", responsable.IdEstandar);
            ViewBag.lstUsuarios = new SelectList(usuarioRepo.GetUsuarios(), "IdUsuario", "Nombre", responsable.IdUsuario);

            try
            {
                var mensaje = repo.ActualizarResponsableEstandar(responsable);
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(responsable);
            }
        }
        #endregion

        #region Delete
        // GET: ResponsableEstandar/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var mensaje = repo.EliminarResponsableEstandar(id);
                TempData["Mensaje"] = mensaje;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar el responsable: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Métodos Auxiliares
        private ResponsableEstandar BuscarResponsable(int id)
        {
            return repo.GetResponsablesEstandares().FirstOrDefault(r => r.IdRespEstandar == id);
        }
        #endregion
    
    }
}
