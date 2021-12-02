using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models.ViewModels
{
    public class DetallesVM
    {
        public DetallesVM()
        {
            Producto = new Productos();
        }

        public Productos Producto { get; set; }
        public bool EnCarrito { get; set; }


    }
}
