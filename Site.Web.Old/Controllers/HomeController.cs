using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using Site2016.Dominio;
namespace Site.Web.Old.Controllers
{
    public class HomeController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Home
        public ActionResult Index()
        {
            var banner = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Include(c => c.ListImagem).OrderByDescending(c => c.DataPublicacao).ToList();

          
            ViewBag.Banner = banner.Take(3);
            ViewBag.Banner2 = banner.Take(8);
            ViewBag.Noticia = banner.Skip(4).Take(6).ToList();
            return View();
        }

        public ActionResult ListadeNoticia(int? pagina)
        {
            int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;


            List<Noticia> n = contexto.Noticia.Include(c => c.TipoNoticiaUnica).OrderByDescending(c => c.DataPublicacao).ToList();

            ViewBag.Noticias = (PagedList<Noticia>)n.ToPagedList(numeroPagina, tamanhoPagina);
            return View();
        }


        public ActionResult Noticias(int id) 
        {
            var n = contexto.Noticia.Where(c => c.Id == id).Include(c => c.ListImagem).Include(c => c.UsuarioUnico).FirstOrDefault();
            ViewBag.Noticias = n;
            var img = n.ListImagem.Where(c => c.tipo == "arq").ToList();
            if (img.Count == 0)
            {
                ViewBag.arq = null;
            }
            else
            {
                ViewBag.arq = img;
            }
            ViewBag.img = n.ListImagem.Where(c => c.tipo != "arq").ToList();


            ViewBag.DataPublicacao = n.DataPublicacao.ToShortDateString();

            return View();
        }
    }
}