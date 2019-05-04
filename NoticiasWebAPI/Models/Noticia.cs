using Microsoft.EntityFrameworkCore; // <-- Referencia necesaria.
using Microsoft.EntityFrameworkCore.Metadata.Builders; // <-- Referencia necesaria.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasWebAPI.Models
{
    /*
        ---------------------------------------------
        MAPEO(Map): Es decirle a entity que relacione 
        los campos que tenemos con la base de datos.
        ---------------------------------------------
    */

    public class Noticia
    {
        public int NoticiaID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public DateTime  Fecha { get; set; }
        public int AutorID { get; set; }
        public Autor Autor { get; set; }

        public class Map {

            // Constructor de mapeo //
            public Map(EntityTypeBuilder<Noticia> ebNoticia) {

                ebNoticia.HasKey(x => x.NoticiaID);
                ebNoticia.Property(x => x.Titulo).HasColumnName("TITULO").HasMaxLength(50);
                ebNoticia.Property(x => x.Descripcion).HasColumnName("DESCRIPCIÓN").HasMaxLength(100);
                ebNoticia.Property(x => x.Contenido).HasColumnName("CONTENIDO").HasMaxLength(int.MaxValue);
                ebNoticia.Property(X => X.Fecha).HasColumnName("FECHA").HasColumnType("Datetime");
                ebNoticia.Property(x => x.AutorID).HasColumnName("AUTOR_ID").HasColumnType("int");
                ebNoticia.HasOne(x => x.Autor);

            }
        }
    }
}
