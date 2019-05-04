using Microsoft.EntityFrameworkCore;
using NoticiasWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasWebAPI
{
    public class NoticiasDBContext: DbContext
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
