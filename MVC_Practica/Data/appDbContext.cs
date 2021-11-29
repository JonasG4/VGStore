using Microsoft.EntityFrameworkCore;
using MVC_Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Practica.Data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {

        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Consolas> Consolas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
    }
}
