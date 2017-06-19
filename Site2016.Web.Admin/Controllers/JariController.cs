using Site2016.Dominio;
using Site2016.InfraEstrutura;
using Site2016.Web.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Site2016.Web.Admin.Security;
namespace Site2016.Web.Admin.Controllers
{
    public class JariController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Jari
        [PermissaoFiltro(Roles = "Jari")]
        public ActionResult Index()
        {

            List<TipoJari> jari = new List<TipoJari>();
            jari = contexto.TipoJari.OrderBy(c => c.Nome).ToList();
            ViewBag.TipoJari = jari;
            return View();
        }


        [PermissaoFiltro(Roles = "Jari")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(FormCollection form, HttpPostedFileBase up)
        {
            try
            {
                int idTipoJari = Convert.ToInt32(form["tipojari"]);
                TipoJari tipojari = contexto.TipoJari.Where(c => c.Id == idTipoJari).FirstOrDefault();
                Jari jari = new Jari();

                jari.DataPublicacao = DateTime.Now;
                jari.Descricao = form["corpo"];
                jari.TipoJariUnico = tipojari;
                var idJari = contexto.Jari.Add(jari);
                contexto.SaveChanges();
                Uteis uteis = new Uteis();
                Arquivo arquivo = uteis.SalvaArqivo(up, idJari.Id);
                jari = new Jari();
                jari = contexto.Jari.Include(c => c.ArquivoUnico).Where(c => c.Id == idJari.Id).FirstOrDefault();
                jari.ArquivoUnico = arquivo;
                contexto.Entry<Jari>(jari).State = EntityState.Modified;
                contexto.SaveChanges();
                return RedirectToAction("index", "Jari");

            }
            catch
            {
                return RedirectToAction("index", "Jari");

            }

        }

        [PermissaoFiltro(Roles = "Jari")]
        public ActionResult ListaJari()
        {
            List<Jari> listaJari = new List<Jari>();
            listaJari = contexto.Jari.Include(c => c.ArquivoUnico).OrderByDescending(c => c.Id).ToList();

            ViewBag.Jari = listaJari;
            return View();
        }

        [PermissaoFiltro(Roles = "Jari")]
        public ActionResult Excluir(int idJari)
        {
            try
            {

                Jari jari = contexto.Jari.Include(c => c.ArquivoUnico).Where(c => c.Id == idJari).FirstOrDefault();

                Uteis uteis = new Uteis();
                var certo = uteis.ExcluirArquivo(jari.ArquivoUnico);

                if (certo == true)
                {

                    AppContexto contextoNovo = new AppContexto();
                    Jari JariNovo = contextoNovo.Jari.Include(c => c.ArquivoUnico).Where(c => c.Id == idJari).FirstOrDefault();


                    contextoNovo.Jari.Remove(JariNovo);
                    contextoNovo.SaveChanges();



                }
                return RedirectToAction("ListaJari", "Jari");
            }
            catch
            {
                return RedirectToAction("ListaJari", "Jari");
            }
        }

        [PermissaoFiltro(Roles = "Jari")]
        public ActionResult AlterarJari(int idJari)
        {
            List<TipoJari> tipoJari = new List<TipoJari>();
            Jari jari = new Jari();
            tipoJari = contexto.TipoJari.OrderBy(c => c.Nome).ToList();
            ViewBag.TipoJari = tipoJari;
            jari = contexto.Jari.Include(c => c.TipoJariUnico).Where(c => c.Id == idJari).FirstOrDefault();
            ViewBag.Jari = jari;
            return View();
        }

        [PermissaoFiltro(Roles = "Jari")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AlterarJari(FormCollection form)
        {
            try
            {
                int idTipoJari = Convert.ToInt32(form["tipojari"]);
                int idJari = Convert.ToInt32(form["idJari"]);
                TipoJari tipojari = contexto.TipoJari.Where(c => c.Id == idTipoJari).FirstOrDefault();
                Jari jari = new Jari();
                jari = contexto.Jari.Where(c => c.Id == idJari).FirstOrDefault();
                jari.DataPublicacao = DateTime.Now;
                jari.Descricao = form["corpo"];
                jari.TipoJariUnico = tipojari;
                contexto.Entry<Jari>(jari).State = EntityState.Modified;
                contexto.SaveChanges();

                return RedirectToAction("ListaJari", "Jari");

            }
            catch
            {
                return RedirectToAction("ListaJari", "Jari");

            }

        }

        [PermissaoFiltro(Roles = "Jari")]
        public ActionResult AlterarArquivo(int idJari)
        {
            List<TipoJari> tipoJari = new List<TipoJari>();
            Jari jari = new Jari();
            tipoJari = contexto.TipoJari.OrderBy(c => c.Nome).ToList();
            ViewBag.TipoJari = tipoJari;
            jari = contexto.Jari.Include(c => c.TipoJariUnico).Where(c => c.Id == idJari).FirstOrDefault();
            ViewBag.Jari = jari;
            return View();
        }

        [PermissaoFiltro(Roles = "Jari")]
        [HttpPost]
        public ActionResult AlterarArquivo(FormCollection form, HttpPostedFileBase up)
        {
            try
            {
                Uteis uteis = new Uteis();
                int idJari = Convert.ToInt32(form["idJari"]);
                var jari = contexto.Jari.Include(c => c.ArquivoUnico).Include(c => c.TipoJariUnico).FirstOrDefault(c => c.Id == idJari);
                Arquivo arquivo = new Arquivo();
                if (jari.ArquivoUnico != null)
                {
                    arquivo = contexto.Arquivo.Where(c => c.Id == jari.ArquivoUnico.Id).FirstOrDefault();
                    uteis.ExcluirArquivo(arquivo);
                }


                Arquivo arquivoNovo = uteis.SalvaArqivo(up, idJari);
                AppContexto contextoNovo = new AppContexto();

                Jari jarinovo = new Jari();
                jarinovo = contextoNovo.Jari.Include(c => c.ArquivoUnico).Include(c => c.TipoJariUnico).FirstOrDefault(c => c.Id == idJari);
                jarinovo.ArquivoUnico = arquivoNovo;
                contextoNovo.Entry<Jari>(jarinovo).State = EntityState.Modified;
                contextoNovo.SaveChanges();
                contextoNovo.Dispose();


                return RedirectToAction("ListaJari", "Jari");
            }
            catch
            {

                return RedirectToAction("ListaJari", "Jari");
            }
        }
    }
}