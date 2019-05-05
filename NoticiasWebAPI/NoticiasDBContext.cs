using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoticiasWebAPI.Controllers;
using NoticiasWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasWebAPI
{
    public class NoticiasDBContext: IdentityDbContext<ApplicationUser> // <-- Permite agregar las tablas del sistema de usuarios de ASP.NET
    {
        public NoticiasDBContext(DbContextOptions opcines) : base(opcines) {

            //..... parametros como la cadena de conexión

        }

        // DB set: Tablas virtuales que maneja Entity para hacer CRUD//
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
      
        // Configura los mapeos //
        protected override void OnModelCreating(ModelBuilder modelB) {

            new Noticia.Map(modelB.Entity<Noticia>());
            new Autor.Map(modelB.Entity<Autor>());
            base.OnModelCreating(modelB);
        }

    }
}
