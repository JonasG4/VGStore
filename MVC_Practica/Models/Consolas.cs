using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models
{
    public class Consolas
    {
        [Key]
        public int IdConsola { get; set; }
        
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        [DisplayName("Nombre de la consola")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="El campo Descripcion es requerido")]
        [MinLength(length:3, ErrorMessage ="La longitud minima de carácteres es 3")]
        public string Descripcion { get; set; }
    }
}
