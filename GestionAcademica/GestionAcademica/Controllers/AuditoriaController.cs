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
    public class AuditoriaController : Controller
    {
        private readonly AuditoriaRepository repo = new AuditoriaRepository();
        private readonly EstandarCalidadRepository selectList = new EstandarCalidadRepository();

        #region Index
        // GET: Auditoria
        public ActionResult Index()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            var auditorias = repo.GetAuditorias();
            return View(auditorias);
        }
        #endregion

        #region Details
        // GET: Auditoria/Details/5
        public ActionResult Details(int id)
        {
            var auditoria = repo.GetAuditorias().FirstOrDefault(a => a.IdAuditoria == id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }
        #endregion

        #region Create
        // GET: Auditoria/Create
        public ActionResult Create()
        {
            ViewBag.lstResultado = new SelectList(ObtenerResultado());
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre");
            return View(new Auditoria());
        }

        // POST: Auditoria/Create
        [HttpPost]
        public ActionResult Create(Auditoria registro)
        {
            ViewBag.lstResultado = new SelectList(ObtenerResultado(), registro.Resultado);
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre", registro.IdEstandar);

            try
            {
                var mensaje = repo.InsertarAuditoria(registro);
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
        // GET: Auditoria/Edit/5
        public ActionResult Edit(int id)
        {
            var auditoria = BuscarAuditoria(id);
            if (auditoria == null)
                return HttpNotFound();
            ViewBag.lstResultado = new SelectList(ObtenerResultado(), auditoria.Resultado);
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre", auditoria.IdEstandar);
            return View(auditoria);
        }

        // POST: Auditoria/Edit/5
        [HttpPost]
        public ActionResult Edit(Auditoria auditoria)
        {
            ViewBag.lstResultado = new SelectList(ObtenerResultado(), auditoria.Resultado);
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre", auditoria.IdEstandar);

            try
            {
                var mensaje = repo.ActualizarAuditoria(auditoria);
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(auditoria);
            }
        }
        #endregion

        #region Delete
        // GET: Auditoria/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var mensaje = repo.EliminarAuditoria(id);
                TempData["Mensaje"] = mensaje;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar la auditoría: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Métodos Auxiliares
        private List<string> ObtenerResultado()
        {
            return new List<string> { "Positivo", "Negativo", "Pendiente" };
        }
        private IEnumerable<EstandarCalidad> ObtenerEstandares()
        {
            return selectList.GetEstandaresCalidad();
        }

        private Auditoria BuscarAuditoria(int id)
        {
            return repo.GetAuditorias().FirstOrDefault(a => a.IdAuditoria == id);
        }
        #endregion
    }
}
