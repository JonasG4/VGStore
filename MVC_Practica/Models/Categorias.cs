using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Este campo es requerido.")]
        [Range(1,int.MaxValue, ErrorMessage ="El rango no es válido")]
        public int Orden { get; set; }
    }
}
