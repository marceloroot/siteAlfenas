using Site2016.Dominio;
using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace Site2016.Web.Admin.Security
{
    public class PermissaoProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            AppContexto contexto = new AppContexto();
            Usuario _usuario = contexto.Usuario.Include(c=>c.ListPermissoes).Where(c => c.Email == username).FirstOrDefault();
            if (_usuario == null)
            {
                return new string[] { };
            }
            //Aqui que pegamos a lista de Permisoes
            List<String> _listPermisoes = new List<string>();
            foreach(var variavel in _usuario.ListPermissoes)
            { 
                _listPermisoes.Add(variavel.Nome);
            }

            return _listPermisoes.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}