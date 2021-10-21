using Microsoft.AspNetCore.Mvc;
using MVC_Practica.Data;
using MVC_Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly appDbContext _db;

        public CategoriasController(appDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Categorias> objlist = _db.Categorias;
            return View(objlist);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categorias obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categorias.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Categorias.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categorias obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categorias.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
         public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Categorias.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
                
        }

        public IActionResult DeletePost(int Id)
        {
            var obj = _db.Categorias.Find(Id);
            _db.Categorias.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
