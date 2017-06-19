using Site2016.Dominio;
using Site2016.Dominio.Contratos;
using Site2016.InfraEstrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Negocio.Classes
{
    public class UsuarioNegocio
    {
        IGenericoMetodos<Usuario> contextoUsuario = new GenericoMetodos<Usuario>();

        public void Salvar(Usuario usuario)
        {
            contextoUsuario.Adicionar(usuario);
        }
    }
}
