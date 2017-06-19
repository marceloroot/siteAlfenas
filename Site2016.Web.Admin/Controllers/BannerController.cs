using Site2016.Dominio;
using Site2016.InfraEstrutura;
using Site2016.Web.Admin.Models;
using Site2016.Web.Admin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site2016.Web.Admin.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Home
        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult Index()
        {
            return View();
        }

        [PermissaoFiltro(Roles = "Banner")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form, List<HttpPostedFileBase> up)
        {
            try
            {
                TipoNoticia tipoN = contexto.TipoNoticia.Where(c => c.Id == 1).FirstOrDefault();
                Usuario usuario = contexto.Usuario.FirstOrDefault();
                var cd = contexto.TipoNoticia.ToList();
                string erro = "";
                Noticia noticia = new Noticia();
                noticia.Corpo = form["corpo"];
                noticia.Titulo = form["titulo"];
                noticia.DataPublicacao = DateTime.Now;
                noticia.TipoNoticiaUnica = tipoN;
                noticia.UsuarioUnico = usuario;


                Uteis uteis = new Uteis();



                ///  noticia.ListImagem.FirstOrDefault().Destaque = true;
                var noticiid = contexto.Noticia.Add(noticia);
                contexto.SaveChanges();
                noticia = new Noticia();
                noticia = contexto.Noticia.Where(c => c.Id == noticiid.Id).FirstOrDefault();

                noticia.ListImagem = (List<Imagem>)uteis.SalvaImagens(up, noticia.Id, 1);

                if (noticia.ListImagem == null)
                {
                    erro = "Os arquivos não possui mais de 1 MB e não foi salvo: ";
                }
                else
                {
                    noticia.ListImagem.FirstOrDefault().Destaque = true;
                    // noticia.ListImagem.FirstOrDefault().Destaque = true;
                    contexto.Entry<Noticia>(noticia).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
                ViewBag.erro = erro;
                return View();

            }
            catch (Exception ex)
            {

                ViewBag.erro = ex.Message.ToString();
                return View();
            }

        }

        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult AlterarNoticias()
        {
            List<Noticia> listaNoticia = new List<Noticia>();
            listaNoticia = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Where(c => c.TipoNoticiaUnica.Id == 1).OrderByDescending(c => c.Id).ToList();

            ViewBag.Noticias = listaNoticia;
            return View();
        }


        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult Excluir(int idNoticia)
        {
            try
            {

                Noticia noticia = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == idNoticia).FirstOrDefault();

                Uteis uteis = new Uteis();
                var certo = uteis.ExcluirListaImagens(noticia.ListImagem);

                if (certo == null)
                {

                    AppContexto contextoNovo = new AppContexto();
                    Noticia noticiaNova = contextoNovo.Noticia.Where(c => c.Id == idNoticia).FirstOrDefault();

                    //noticiaNova.ListImagem = certo;
                    contextoNovo.Noticia.Remove(noticiaNova);
                    contextoNovo.SaveChanges();
                    contextoNovo.Dispose();
                }
                return RedirectToAction("AlterarNoticias", "Banner");
            }
            catch
            {
                return RedirectToAction("AlterarNoticias", "Banner");
            }
        }


        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult PaginaAlterarNoticia(int IdNoticia)
        {
            Noticia noticia = new Noticia();
            noticia = contexto.Noticia.Where(c => c.Id == IdNoticia).FirstOrDefault();
            ViewBag.Noticia = noticia;

            return View();
        }


        [PermissaoFiltro(Roles = "Banner")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PaginaAlterarNoticia(FormCollection form)
        {
            try
            {
                int idNoticia = Convert.ToInt32(form["Id"]);
                Usuario usuario = contexto.Usuario.FirstOrDefault();
                string erro = "";
                Noticia noticia = contexto.Noticia.Include(c => c.UsuarioUnico).Include(c => c.TipoNoticiaUnica).Include(c => c.ListImagem).Where(c => c.Id == idNoticia).FirstOrDefault();


                noticia.Corpo = form["corpo"];
                noticia.Titulo = form["titulo"];


                noticia.UsuarioUnico = usuario;


                Uteis uteis = new Uteis();
                contexto.Entry<Noticia>(noticia).State = System.Data.Entity.EntityState.Modified;

                contexto.SaveChanges();

                ViewBag.erro = erro;

                return RedirectToAction("AlterarNoticias", "Banner");

            }
            catch (Exception ex)
            {

                ViewBag.erro = ex.Message.ToString();
                return RedirectToAction("AlterarNoticias", "Banner");
            }

        }



        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult InserirImagem(int idNoticia)
        {

            Noticia noticia = new Noticia();
            noticia = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == idNoticia).FirstOrDefault();
            ViewBag.listaImagem = (List<Imagem>)noticia.ListImagem.ToList();
            ViewBag.IdNoticia = noticia.Id;
            ViewBag.IdImg = noticia.ListImagem.LastOrDefault().Id;
            return View();
        }


        [PermissaoFiltro(Roles = "Banner")]
        [HttpPost]
        public ActionResult InserirImagem(FormCollection form, List<HttpPostedFileBase> up)
        {

            int idNoticia = Convert.ToInt32(form["Id"]);
            int idImagem = Convert.ToInt32(form["IdImg"]);
            string erro = "";
            try
            {
                Noticia noticia = new Noticia();
                Uteis uteis = new Uteis();

                noticia = contexto.Noticia.Where(c => c.Id == idNoticia).FirstOrDefault();
                noticia.ListImagem = (List<Imagem>)uteis.SalvaImagens(up, idNoticia, idImagem);

                if (noticia.ListImagem == null)
                {
                    erro = "Os arquivos não possui mais de 1 MB e não foi salvo: ";
                }
                else
                {

                    // noticia.ListImagem.FirstOrDefault().Destaque = true;
                    contexto.Entry<Noticia>(noticia).State = EntityState.Modified;
                    contexto.SaveChanges();

                }
                ViewBag.erro = erro;
                ViewBag.IdImg = noticia.ListImagem.LastOrDefault().Id;
                noticia = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == idNoticia).FirstOrDefault();
                ViewBag.listaImagem = noticia.ListImagem.ToList();
                ViewBag.IdNoticia = noticia.Id;

                return RedirectToAction("AlterarNoticias", "Banner");
            }
            catch
            {
                Noticia noticia = new Noticia();

                noticia = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == idNoticia).FirstOrDefault();
                ViewBag.listaImagem = (List<Imagem>)noticia.ListImagem.ToList();
                ViewBag.IdNoticia = noticia.Id;
                return RedirectToAction("AlterarNoticias", "Banner");

            }
        }

        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult ExcluirFoto(int idImg)
        {
            try
            {
                Imagem img = new Imagem();
                img = contexto.Imagem.Where(c => c.Id == idImg).FirstOrDefault();
                Uteis uteis = new Uteis();
                bool ok = uteis.ExcluirImagem(img.Caminho);
                if (ok == true)
                {
                    contexto.Imagem.Remove(img);
                    contexto.SaveChanges();

                }
                return RedirectToAction("AlterarNoticias", "Banner");
            }
            catch
            {
                return RedirectToAction("AlterarNoticias", "Banner");
            }
        }

        [PermissaoFiltro(Roles = "Banner")]
        public ActionResult DestacarImg(int id, int idDestaque)
        {
            try
            {
                Noticia noticia = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == idDestaque).FirstOrDefault();
                var img = noticia.ListImagem.Where(c => c.Destaque == true).FirstOrDefault();
                noticia.ListImagem.Where(c => c.Id == img.Id).FirstOrDefault().Destaque = false;

                noticia.ListImagem.Where(c => c.Id == id).FirstOrDefault().Destaque = true;

                contexto.Entry<Noticia>(noticia).State = EntityState.Modified;
                contexto.SaveChanges();
                return RedirectToAction("AlterarNoticias", "Banner");
            }
            catch
            {
                return RedirectToAction("AlterarNoticias", "Banner");
            }
        }
    }
}