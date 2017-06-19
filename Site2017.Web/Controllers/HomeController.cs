using Site2016.Dominio;
using Site2016.Dominio.Contratos;
using Site2016.InfraEstrutura;
using Site2016.InfraEstrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace Site2017.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        protected readonly AppContexto contexto = new AppContexto();
        public ActionResult Index()
        {
            var banner = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Include(c => c.ListImagem).OrderByDescending(c => c.DataPublicacao).ToList();
            ViewBag.Banner = banner.Take(4);
            ViewBag.Noticia = banner.Skip(4).Take(3).OrderByDescending(c=>c.Id).ToList();
            var s = banner.FirstOrDefault().Titulo.Count();
          

            ViewBag.Noticia2 = banner.Skip(7).Take(3).OrderByDescending(c => c.Id).ToList();

            ViewBag.Projeto = banner.Take(8);
            return View();
           
        }

        #region Noticia
        public ActionResult Noticias(int id)
        {
            ViewBag.Noticias = contexto.Noticia.Where(c => c.Id == id).FirstOrDefault();
            var n = contexto.Noticia.Where(c => c.Id == id).Include(c => c.ListImagem).Include(c => c.UsuarioUnico).FirstOrDefault();
            ViewBag.Noticias = n;
           
            ViewBag.DataPublicacao = n.DataPublicacao.ToShortDateString();

            return View();
        }

        public ActionResult ListaNoticias(int? pagina)
        {
            int tamanhoPagina = 20;
            int numeroPagina = pagina ?? 1;


            List<Noticia> n = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Where(c => c.TipoNoticiaUnica.Nome == "Noticia").OrderByDescending(c=>c.DataPublicacao).ToList();

            ViewBag.Noticias = (PagedList<Noticia>)n.ToPagedList(numeroPagina, tamanhoPagina);
            return View();
        }

        [HttpPost]
        public ActionResult ListaNoticias(int? pagina, FormCollection form)
        {
            string busca = form["search"].ToString();


            int tamanhoPagina = 20;
            int numeroPagina = pagina ?? 1;

            if (busca == "")
            {

                List<Noticia> n = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Where(c => c.TipoNoticiaUnica.Nome == "Noticia").ToList();

                ViewBag.Noticias = (PagedList<Noticia>)n.ToPagedList(numeroPagina, tamanhoPagina);
                return View();
            }
            else
            {
                List<Noticia> n = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Where(c => c.Titulo.Contains(busca)).Where(c => c.TipoNoticiaUnica.Nome == "Noticia").ToList();

                ViewBag.Noticias = (PagedList<Noticia>)n.ToPagedList(numeroPagina, tamanhoPagina);
                return View();

            }

        }
        #endregion


        #region Secretarias


        public ActionResult ListaSecretaria(int? pagina)
        {
            int tamanhoPagina = 15;
            int numeroPagina = pagina ?? 1;

            List<Secretaria> n = contexto.Secretaria.Include(c => c.SecretariaUnica).Include(c => c.ListTelefone).ToList();

            ViewBag.Noticias = (PagedList<Secretaria>)n.ToPagedList(numeroPagina, tamanhoPagina);
            return View();

        }
        [HttpPost]
        public ActionResult ListaSecretaria(int? pagina, FormCollection form)
        {
            string busca = form["search"].ToString();

            int tamanhoPagina = 15;
            int numeroPagina = pagina ?? 1;
            if (busca == "")
            {
                List<Secretaria> n = contexto.Secretaria.Include(c => c.SecretariaUnica).Include(c => c.ListTelefone).ToList();

                ViewBag.Noticias = (PagedList<Secretaria>)n.ToPagedList(numeroPagina, tamanhoPagina);
                return View();
            }
            else
            {
                List<Secretaria> n = contexto.Secretaria.Include(c => c.SecretariaUnica).Include(c => c.ListTelefone).Where(c => c.Nome.Contains(busca) || c.Endereco.Contains(busca)).ToList();

                ViewBag.Noticias = (PagedList<Secretaria>)n.ToPagedList(numeroPagina, tamanhoPagina);
                return View();

            }



        }


        public ActionResult Secretaria(int idSec)
        {
            Secretaria secretaria = new Secretaria();
            secretaria = contexto.Secretaria.Where(c => c.Id == idSec).Include(c => c.ListTelefone).Include(c => c.LsitaSubSecretarias).FirstOrDefault();
            ViewBag.secretaria = secretaria;
            return View();

        }


        #endregion

        #region telefone
        public ActionResult Telefones(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            //ViewBag.telefones = contexto.Telefone.Include(c => c.SecretariaUnica).Include(c => c.SubSecretariaUnica).ToPagedList(numeroPagina,tamanhoPagina);
            List<Telefone> n = contexto.Telefone.Include(c => c.SecretariaUnica).OrderBy(c => c.SecretariaUnica.Nome).ToList();
            ViewBag.telefones = (PagedList<Telefone>)n.ToPagedList(numeroPagina, tamanhoPagina);


            return View();
        }

        [HttpPost]
        public ActionResult Telefones(int? pagina, FormCollection form)
        {
            string busca = form["search"].ToString();

            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            if (busca == "")
            {

                //ViewBag.telefones = contexto.Telefone.Include(c => c.SecretariaUnica).Include(c => c.SubSecretariaUnica).ToPagedList(numeroPagina,tamanhoPagina);
                List<Telefone> n = contexto.Telefone.Include(c => c.SecretariaUnica).OrderBy(c => c.SecretariaUnica.Nome).ToList();
                ViewBag.telefones = (PagedList<Telefone>)n.ToPagedList(numeroPagina, tamanhoPagina);
                return View();
            }
            else
            {
                //ViewBag.telefones = contexto.Telefone.Include(c => c.SecretariaUnica).Include(c => c.SubSecretariaUnica).ToPagedList(numeroPagina,tamanhoPagina);
                List<Telefone> n = contexto.Telefone.Include(c => c.SecretariaUnica).OrderBy(c => c.SecretariaUnica.Nome).ToList();
                List<Telefone> i = n.Where(c => c.Numero.Contains(busca)).OrderBy(c => c.SecretariaUnica.Nome).ToList();
                if (i.Count <= 0)
                {
                    n = n.Where(c => c.SecretariaUnica.Endereco.Contains(busca) || c.SecretariaUnica.Nome.Contains(busca)).OrderBy(c => c.SecretariaUnica.Nome).ToList();
                }
                else
                {
                    n = i;
                }


                ViewBag.telefones = (PagedList<Telefone>)n.ToPagedList(numeroPagina, tamanhoPagina);


                return View();
            }

        }
        #endregion

        public ActionResult EmissaoAlvara()
        {

            return View();
        }

        public ActionResult AreaAzul()
        {

            return View();
        }

        public ActionResult TermoConstrucao()
        {

            return View();
        }

        public ActionResult CertidaoNegativo()
        {

            return View();
        }

        public ActionResult Manutencao()
        {

            return View();
        }
    }
}
