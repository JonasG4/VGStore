using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        public string Nombre { get; set; }


        [Required(ErrorMessage ="Este campo es requerido.")]
        public String DescripcionCorta { get; set; }
        [Required(ErrorMessage ="Este campo es requerido.")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        [Range(1, int.MaxValue,ErrorMessage ="El valo debe ser mayor que 0")]
        public double Precio { get; set; }
        public string Imagen { get; set; }
        [Display(Name = "Categorias")]
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categorias Categorias  { get; set; }

        [Display(Name = "Consola")]
        public int IdConsola { get; set; }
        [ForeignKey("IdConsola")]
        public virtual Consolas Consolas  { get; set; }



        //Relacion con la tabla producto
        [Display(Name = "Tipo")]
        public int IdTipo { get; set; }
        [ForeignKey("IdTipo")]
        public virtual Tipo Tipo { get; set; }
    }
}
