using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio.Contratos
{
    public interface IGenericoMetodos<T> where T:class
    {
        void Adicionar(T item);
        void Editar(T item);
        void Remover(T item);
        List<T> Buscar();
        T BuscarId(int id);
    }
}
