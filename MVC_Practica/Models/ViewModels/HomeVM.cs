using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Productos> Productos { get; set; }
        public IEnumerable<Consolas>  Consolas  { get; set; }
        public IEnumerable<Categorias>  Categorias  { get; set; }
        public IEnumerable<Tipo>  Tipo  { get; set; }

    }
}
