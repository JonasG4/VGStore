using MVC_Practica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Practica.Data;
using MVC_Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MVC_Practica.Controllers
{
    public class ProductosController : Controller
    {
        private readonly appDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductosController(appDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Productos> objlist = _db.Productos;

            foreach (var obj in objlist)
            {
                obj.Categorias = _db.Categorias.FirstOrDefault(u => u.Id == obj.IdCategoria);
                obj.Consolas = _db.Consolas.FirstOrDefault(u => u.IdConsola == obj.IdConsola);
                obj.Tipo= _db.Tipo.FirstOrDefault(u => u.IdTipo == obj.IdTipo);
            }

            return View(objlist);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? Id)
        {
            ProductosVM productosVM = new ProductosVM()
            {
                Productos = new Productos(),
                CategoriasSelectList = _db.Categorias.Select(i => new SelectListItem {
                    Text = i.Nombre,
                    Value = i.Id.ToString()           
            }),
                ConsolasSelectList = _db.Consolas.Select(i => new SelectListItem {
                    Text = i.Nombre,
                    Value = i.IdConsola.ToString()           
            }),
                TipoSelectList = _db.Tipo.Select(i => new SelectListItem {
                    Text = i.Nombre,
                    Value = i.IdTipo.ToString()           
            }),

            };

            if (Id == null)
            {   
                //Crear producto
                return View(productosVM);
            }
            else
            {
                //Editar producto
                productosVM.Productos = _db.Productos.Find(Id);
                if (productosVM.Productos == null)
            {
                return NotFound();
            }
            return View(productosVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductosVM productosVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webrootPath = _webHostEnvironment.WebRootPath;

                if(productosVM.Productos.IdProducto == 0)
                {
                    //Crear
                    string upload = webrootPath + WC.ProductosPath; //Nueva ubicacion del archivo
                    string filename = Guid.NewGuid().ToString(); //Nombre generado
                    string extension = Path.GetExtension(files[0].FileName); //Extraer la extension del archivo

                    using (var filestream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    productosVM.Productos.Imagen = filename + extension;

                    _db.Productos.Add(productosVM.Productos);
                    _db.SaveChanges();
                }
                else
                {
                    //Editar
                    var objDB = _db.Productos.AsNoTracking().FirstOrDefault(u => u.IdProducto == productosVM.Productos.IdProducto);

                    if(files.Count > 0)
                    {
                        //Se actualiza la imagen

                        string upload = webrootPath + WC.ProductosPath; //Nueva ubicacion del archivo
                        string filename = Guid.NewGuid().ToString(); //Nombre generado
                        string extension = Path.GetExtension(files[0].FileName); //Extraer la extension del archivo

                        var oldFile = Path.Combine(upload, objDB.Imagen);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var filestream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }

                        productosVM.Productos.Imagen = filename + extension;
                    }
                    else
                    {
                        //No se actualiza la imagen
                        productosVM.Productos.Imagen = objDB.Imagen;
                    }

                    _db.Productos.Update(productosVM.Productos);
                    _db.SaveChanges();
                }

                    productosVM.CategoriasSelectList = _db.Categorias.Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Id.ToString()
                    });
                     productosVM.ConsolasSelectList = _db.Consolas.Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.IdConsola.ToString()
                    });
                    
                    productosVM.TipoSelectList = _db.Tipo.Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.IdTipo.ToString()
                    });

                return RedirectToAction("Index");
            }

            return View(productosVM);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Productos producto = _db.Productos.Include(u => u.Categorias).Include(u => u.Consolas).Include(u => u.Tipo).FirstOrDefault(u => u.IdProducto == Id) ; //eager loading
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        public IActionResult DeletePost(int IdProducto)
        {
            var obj = _db.Productos.Find(IdProducto);

            if(obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment + WC.ProductosPath; // Ubicacion del archivo
            

            var oldFile = Path.Combine(upload, obj.Imagen);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Productos.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
