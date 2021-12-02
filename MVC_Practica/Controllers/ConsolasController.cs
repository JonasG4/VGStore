using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Practica.Data;
using MVC_Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Controllers
{
    [Authorize(Roles = WC.AdminRole)]

    public class ConsolasController : Controller
    {
        private readonly appDbContext _db;

        public ConsolasController(appDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Consolas> objlist = _db.Consolas;
            return View(objlist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Consolas obj)
        {
            if (ModelState.IsValid)
            {
                _db.Consolas.Add(obj);
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

            var obj = _db.Consolas.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Consolas obj)
        {
            if (ModelState.IsValid)
            {
                _db.Consolas.Update(obj);
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

            var obj = _db.Consolas.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult DeletePost(int IdConsola)
        {
            var obj = _db.Consolas.Find(IdConsola);
            _db.Consolas.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
 