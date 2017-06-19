using Site2016.Dominio;
using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Site2016.Web.Admin.Models
{
    public class UsuarioFront
    {
        public bool AutenticarUsuario(string email, string senha)
        {
            AppContexto _contexto = new AppContexto();
            Usuario _usuario = new Usuario();
            _usuario = _contexto.Usuario.Where(c => c.Email == email).FirstOrDefault();
            _contexto.Dispose();
            if (_usuario == null)
                return false;
            FormsAuthentication.SetAuthCookie(_usuario.Email, false);
            return true;
        }

        public Usuario BuscarUsuarioLogado()
        {
            try
            {
                AppContexto _contexto = new AppContexto();
                Usuario _usuario = new Usuario();
                string email = HttpContext.Current.User.Identity.Name;
                _usuario = _contexto.Usuario.Where(c => c.Email == email).FirstOrDefault();
                return _usuario;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao buscar usuario logado");

            }

        }

        public void Deslogar()
        {
            FormsAuthentication.SignOut();

        }
    }
}