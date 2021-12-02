using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Models
{
    public class AppUsuario : IdentityUser
    {
        public string FullName { get; set; }
    }
}
