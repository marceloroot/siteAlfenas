using Site2016.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.InfraEstrutura.Repositorio
{
    public class GenericoMetodos<T> : IDisposable, IGenericoMetodos<T> where T:class 
    {
        protected readonly AppContexto contexto = new AppContexto();


        public void Adicionar(T item)
        {
            contexto.Set<T>().Add(item);
            contexto.SaveChanges();
        }

        public void Editar(T item)
        {
            contexto.Entry(item).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Remover(T item)
        {
            contexto.Set<T>().Remove(item);
            contexto.SaveChanges();
        }

        public List<T> Buscar()
        {
            return contexto.Set<T>().ToList();
        }

        public T BuscarId(int id)
        {
            return contexto.Set<T>().Find(id);
        }


        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
