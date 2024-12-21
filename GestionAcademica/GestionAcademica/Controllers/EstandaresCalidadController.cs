using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Models;
using GestionAcademica.Repositories;
using Microsoft.Win32;

namespace GestionAcademica.Controllers
{
    public class EstandaresCalidadController : Controller
    {
        // Instancia del repositorio
        private readonly EstandarCalidadRepository repo = new EstandarCalidadRepository();

        #region Index
        // GET: EstandaresCalidad
        public ActionResult Index()
        {
            // Mensajes de TempData
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            var estandares = repo.GetEstandaresCalidad(); // Obtener lista
            return View(estandares);
        }
        #endregion

        #region Details
        // GET: EstandaresCalidad/Details/5
        public ActionResult Details(int id)
        {
            var estandar = repo.GetEstandaresCalidad().FirstOrDefault(e => e.IdEstandar == id);
            if (estandar == null)
            {
                return HttpNotFound();
            }
            return View(estandar);
        }
        #endregion

        #region Create
        // GET: EstandaresCalidad/Create
        public ActionResult Create()
        {
            ViewBag.lstEstado = new SelectList(ObtenerEstados());
            return View(new EstandarCalidad());
        }

        // POST: EstandaresCalidad/Create
        [HttpPost]
        public ActionResult Create(EstandarCalidad registro)
        {
            ViewBag.lstEstado = new SelectList(ObtenerEstados(), registro.Estado);

            try
            {
                var mensaje = repo.InsertarEstandarCalidad(registro); // Guardar registro
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(registro);
            }
        }
        #endregion

        #region Edit
        // GET: EstandaresCalidad/Edit/5
        public ActionResult Edit(int id)
        {
            var estandar = BuscarEstandar(id);
            if (estandar == null) return HttpNotFound();

            ViewBag.lstEstado = new SelectList(ObtenerEstados(), estandar.Estado);
            return View(estandar);
        }

        // POST: EstandaresCalidad/Edit/5
        [HttpPost]
        public ActionResult Edit(EstandarCalidad estandar)
        {
            ViewBag.lstEstado = new SelectList(ObtenerEstados(), estandar.Estado);

            try
            {
                var mensaje = repo.ActualizarEstandarCalidad(estandar); // Actualizar registro
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(estandar);
            }
        }
        #endregion

        #region Delete
        // GET: EstandaresCalidad/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var mensaje = repo.EliminarEstandarCalidad(id);
                TempData["Mensaje"] = mensaje;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar el estándar: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Métodos Auxiliares
        private List<string> ObtenerEstados()
        {
            return new List<string> { "Completado", "En Proceso", "Pendiente" };
        }

        private EstandarCalidad BuscarEstandar(int id)
        {
            return repo.GetEstandaresCalidad().FirstOrDefault(e => e.IdEstandar == id);
        }
        #endregion
    }
}
