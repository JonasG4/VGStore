using Microsoft.AspNetCore.Mvc;
using MVC_Practica.Data;
using MVC_Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Controllers
{
    public class TipoController : Controller
    {
        private readonly appDbContext _db;

        public TipoController(appDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Tipo> objList = _db.Tipo;


            return View(objList);
        }

    }
}
