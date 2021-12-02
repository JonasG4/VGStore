using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_Practica.Data;
using MVC_Practica.Models;
using MVC_Practica.Models.ViewModels;
using MVC_Practica.Utily;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly appDbContext _db;

        public HomeController(ILogger<HomeController> logger, appDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Productos = _db.Productos.Include(u => u.Categorias).Include(u => u.Consolas).Include(u => u.Tipo),
                Consolas = _db.Consolas,
                Categorias = _db.Categorias,
                Tipo = _db.Tipo
            };
            return View(homeVM);
        }

        public IActionResult Detalles(int id)
        {
            List<CarritoCompras> ListaCarrito = new List<CarritoCompras>();
            if (HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito) != null && HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito).Count() > 0)
            {
                ListaCarrito = HttpContext.Session.Get<List<CarritoCompras>>(WC.SesionCarrito);
            }

            DetallesVM DetallesVM = new DetallesVM()
            {
                Producto = _db.Productos.Include(u => u.Categorias).Include(u => u.Consolas).Where(u => u.IdProducto == id).FirstOrDefault(),
                EnCarrito = false
            };

            foreach(var item in ListaCarrito)
            {
                if(item.ProductoId == id)
                {
                    DetallesVM.EnCarrito = true;
                }
            }

            return View(DetallesVM);
        }

        [HttpPost, ActionName("Detalles")]
        public IActionResult DetallesPost(int id)
        {
            List<CarritoCompras> ListaCarrito = new List<CarritoCompras>();
            if(HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito) != null && HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito).Count() > 0)
            {
                ListaCarrito = HttpContext.Session.Get<List<CarritoCompras>>(WC.SesionCarrito);
            }

            ListaCarrito.Add(new CarritoCompras { ProductoId = id });

            HttpContext.Session.Set(WC.SesionCarrito, ListaCarrito);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoverCarrito(int id)
        {
            List<CarritoCompras> ListaCarrito = new List<CarritoCompras>();
            if(HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito) != null && HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito).Count() > 0)
            {
                ListaCarrito = HttpContext.Session.Get<List<CarritoCompras>>(WC.SesionCarrito);
            }

            var itemRemover = ListaCarrito.SingleOrDefault(r => r.ProductoId == id);
            
            if(itemRemover != null)
            {
                ListaCarrito.Remove(itemRemover);
            }

            HttpContext.Session.Set(WC.SesionCarrito, ListaCarrito);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
