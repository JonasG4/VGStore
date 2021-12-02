using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Practica.Data;
using MVC_Practica.Models;
using MVC_Practica.Utily;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly appDbContext _db;

        public CarritoController(appDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<CarritoCompras> ListaCarrito = new List<CarritoCompras>();
            if (HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito) != null && HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito).Count() > 0)
            {
                //Session Existe
                ListaCarrito = HttpContext.Session.Get<List<CarritoCompras>>(WC.SesionCarrito);

            }

            List<int> productosEnCarrito = ListaCarrito.Select(u => u.ProductoId).ToList();
            IEnumerable<Productos> prodList = _db.Productos.Where(u => productosEnCarrito.Contains(u.IdProducto));
            return View(prodList);
        }

        public IActionResult Remover(int id)
        {
            List<CarritoCompras> ListaCarrito = new List<CarritoCompras>();
            if (HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito) != null && HttpContext.Session.Get<IEnumerable<CarritoCompras>>(WC.SesionCarrito).Count() > 0)
            {
                //Session Existe
                ListaCarrito = HttpContext.Session.Get<List<CarritoCompras>>(WC.SesionCarrito);

            }

            ListaCarrito.Remove(ListaCarrito.FirstOrDefault(u => u.ProductoId == id));
            HttpContext.Session.Set(WC.SesionCarrito, ListaCarrito);
            return RedirectToAction(nameof(Index));
        }
    }
}
