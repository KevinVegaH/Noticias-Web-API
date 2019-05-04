using Microsoft.EntityFrameworkCore;
using NoticiasWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasWebAPI.Servicios
{
    public class NoticiasServices
    {
        public readonly NoticiasDBContext _NoticiasDB; // <-- Definimos que el DBcontext solo será de lectura.

        public NoticiasServices(NoticiasDBContext noticiasDB) {

            _NoticiasDB = noticiasDB; // <-- Creamos nuestra inyección de independencias. Inyecctamos el DBcontext

        }

        public List<Noticia> verListadoTodasLasNoticias() {

            /* 
            ----------------------------
             Devuelve la noticia numero 1:
             
            var NoticiaBuscada = _NoticiasDB.Noticia.Where(x => x.NoticiaID == 1);
            
             Nota: cambiar 'List<Noticia>' por 'Noticia'.             
            ------------------------------
            
            ------------------------------
             Devuelve un listado de noticias del autor definido por el ID:

             var NoticiaBuscada = _NoticiasDB.Noticia.Where(x => x.AutorID == 1).toList();
            ------------------------------             
             */

            var NoticiaBuscada = _NoticiasDB.Noticia.Include(x => x.Autor).OrderByDescending(x => x.NoticiaID).ToList();

            return NoticiaBuscada;
        }

        public bool Agregar(Noticia NoticiaAgregar) {

            try
            {
                _NoticiasDB.Noticia.Add(NoticiaAgregar);
                _NoticiasDB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar(Noticia NoticiaEditar)
        {

            try
            {
                var noticia = _NoticiasDB.Noticia.FirstOrDefault(x => x.NoticiaID == NoticiaEditar.NoticiaID); // <-- recibimos la primera noticia que se encuentre con el paramentro y la almacenamos en la variable noticia.
                noticia.Titulo = NoticiaEditar.Titulo;
                noticia.Descripcion = NoticiaEditar.Descripcion;
                noticia.Contenido = NoticiaEditar.Contenido;
                noticia.Fecha = NoticiaEditar.Fecha;
                noticia.AutorID = NoticiaEditar.AutorID;
                _NoticiasDB.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Eliminar(int NoticiaID) {

            try
            {
                var noticiaEliminar = _NoticiasDB.Noticia.FirstOrDefault(x => x.NoticiaID == NoticiaID);
                _NoticiasDB.Noticia.Remove(noticiaEliminar);
                _NoticiasDB.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
