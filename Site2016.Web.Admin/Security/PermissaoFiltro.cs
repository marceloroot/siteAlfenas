using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site2016.Web.Admin.Security
{
    public class PermissaoFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //Cado o usuario não for indentificado vai para uma pagina de negado
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Negado");
            }
        }
    }
}
   