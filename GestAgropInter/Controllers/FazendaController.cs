using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace GestAgropInter.Controllers
{
    public class FazendaController : Controller
    {
        private readonly IFazendaRepository _fazenda;


        public FazendaController(IFazendaRepository fazenda)
        {
            _fazenda = fazenda;
        }

        public IActionResult Index()
        {
            var fazendas = _fazenda.GetAllFazenda();
            return View(fazendas);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fazenda fazenda = _fazenda.GetFazenda(id);

            if (fazenda == null)
            {
                return NotFound();
            }
            return View(fazenda);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Fazenda fazenda)
        {

            if (ModelState.IsValid)
            {
                if (fazenda.Id != null)
                {
                    fazenda.DataAlteracao = DateTime.Now;
                    _fazenda.UpdateFazenda(fazenda);
                }                    
                else
                    _fazenda.AddFazenda(fazenda);
                return RedirectToAction("Index");
            }
            return View(fazenda);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Fazenda fazenda = _fazenda.GetFazenda(id);

            if (fazenda == null)
            {
                return NotFound();
            }
            return View(fazenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Fazenda fazenda)
        {
            if (id != fazenda.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _fazenda.UpdateFazenda(fazenda);
                return RedirectToAction("Index");
            }
            return View(fazenda);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Fazenda fazenda = _fazenda.GetFazenda(id);

            if (fazenda == null)
            {
                return NotFound();
            }
            return View(fazenda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _fazenda.DeleteFazenda(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}