using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site2016.Web.Admin.Controllers
{
    public class ImagensController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Imagens
        public FileResult Imagem(int id)
        {
            if (id != null)
            {
                var imagem = contexto.Imagem.Where(c => c.Id == id).FirstOrDefault();
                if (imagem != null)
                {
                    var arquivoDeImagem = Server.MapPath("~/Uploads/Img/" + imagem.Caminho);
                    return File(arquivoDeImagem, "image/jpeg");
                }
            }

            return null;
        }
    }
}