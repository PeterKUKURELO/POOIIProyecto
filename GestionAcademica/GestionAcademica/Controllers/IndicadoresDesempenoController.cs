using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAcademica.Models;
using GestionAcademica.Repositories;

namespace GestionAcademica.Controllers
{
    public class IndicadoresDesempenoController : Controller
    {
        // Instancia del repositorio
        private readonly IndicadorDesempenoRepository repo = new IndicadorDesempenoRepository();
        private readonly EstandarCalidadRepository selectList = new EstandarCalidadRepository();
        #region Index
        public ActionResult Index()
        {
            // Mensajes de TempData
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            var indicadores = repo.GetIndicadorDesempeno(); // Obtener lista
            return View(indicadores);
        }
        #endregion

        #region Details
        // GET: IndicadoresDesempeno/Details/5
        public ActionResult Details(int id)
        {
            var indicador = repo.GetIndicadorDesempeno().FirstOrDefault(e => e.IdIndicador == id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }
        #endregion

        #region Create
        // GET: IndicadoresDesempeno/Create
        public ActionResult Create()
        {
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre");
            return View(new IndicadorDesempeno());
        }

        // POST: IndicadoresDesempeno/Create
        [HttpPost]
        public ActionResult Create(IndicadorDesempeno registro)
        {
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre", registro.IdEstandar);

            try
            {
                var mensaje = repo.InsertarIndicadorDesempeno(registro); // Guardar registro
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
        // GET: IndicadoresDesempeno/Edit/5
        public ActionResult Edit(int id)
        {
            var indicador = BuscarIndicador(id);
            if (indicador == null) return HttpNotFound();

            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre");
            return View(indicador);
        }

        // POST: IndicadoresDesempeno/Edit/5
        [HttpPost]
        public ActionResult Edit(IndicadorDesempeno indicador)
        {
            ViewBag.lstEstandares = new SelectList(ObtenerEstandares(), "IdEstandar", "Nombre");
            try
            {
                var mensaje = repo.ActualizarIndicadorDesempeno(indicador); // Actualizar registro
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(indicador);
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            try
            {
                var mensaje = repo.EliminarIndicadorDesempeno(id);
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
        private IEnumerable<EstandarCalidad> ObtenerEstandares()
        {
            return selectList.GetEstandaresCalidad(); // Método del repositorio que obtiene estándares
        }
        private IndicadorDesempeno BuscarIndicador(int id)
        {
            return repo.GetIndicadorDesempeno().FirstOrDefault(e => e.IdIndicador == id);
        }
        #endregion
    }
}
