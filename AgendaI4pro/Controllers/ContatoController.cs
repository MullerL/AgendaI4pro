using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaI4pro.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaI4pro.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoDAL _contato;
        public ContatoController(IContatoDAL contato)
        {
            _contato = contato;
        }
        public IActionResult Index()
        {
            List<Contato> listaContatos = new List<Contato>();
            listaContatos = _contato.GetAllContato().ToList();
            return View(listaContatos);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato Contato = _contato.GetContato(id);
            if (Contato == null)
            {
                return NotFound();
            }
            return View(Contato);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Contato Contato)
        {
            if (ModelState.IsValid)
            {
                _contato.AddContato(Contato);
                return RedirectToAction("Index");
            }
            return View(Contato);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato Contato = _contato.GetContato(id);
            if (Contato == null)
            {
                return NotFound();
            }
            return View(Contato);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Contato Contato)
        {
            if (id != Contato.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _contato.UpdateContato(Contato);
                return RedirectToAction("Index");
            }
            return View(Contato);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contato Contato = _contato.GetContato(id);
            if (Contato == null)
            {
                return NotFound();
            }
            return View(Contato);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _contato.DeleteContato(id);
            return RedirectToAction("Index");
        }
    }
}
