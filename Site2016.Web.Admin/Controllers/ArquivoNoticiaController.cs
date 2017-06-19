using Site2016.Dominio;
using Site2016.InfraEstrutura;
using Site2016.Web.Admin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using Site2016.Web.Admin.Models;

namespace Site2016.Web.Admin.Controllers
{
    public class ArquivoNoticiaController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: ArquivoNoticia
        [PermissaoFiltro(Roles = "Noticia")]
        public ActionResult Index()
        {

            List<Noticia> listaNoticia = new List<Noticia>();
            listaNoticia = contexto.Noticia.Include(c => c.TipoNoticiaUnica).Where(c => c.TipoNoticiaUnica.Id == 2).OrderByDescending(c => c.Id).ToList();

            ViewBag.Noticias = listaNoticia;
            return View();
        }

         [PermissaoFiltro(Roles = "Noticia")]
        public ActionResult AdicionarArquivo(int id)
        {
            try
            {
                List<Imagem> list = contexto.Imagem.Include(c => c.ListTipoImagem).ToList();
                List<TipoImagem> listtipo = contexto.TipoImagem.Include(c => c.ListImagem).Where(c => c.Id == 1).ToList();
                Noticia noticia = new Noticia();
                noticia = contexto.Noticia.Where(c => c.Id == id).FirstOrDefault();
                ViewBag.noticia = noticia;
                
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }

        [PermissaoFiltro(Roles = "Noticia")]
        [HttpPost]
        public ActionResult AdicionarArquivo(FormCollection form, HttpPostedFileBase up)
        {
            try
            {
                int id = Convert.ToInt32(form["idNoticia"].ToString());
                string descricao = form["corpo"].ToString();
                string titulo = form["titulo"].ToString();
                Noticia noticia = new Noticia();
                noticia = contexto.Noticia.Where(c => c.Id == id).Include(c=>c.ListImagem).FirstOrDefault();
                if(noticia !=null)
                {
                    UsuarioFront front = new UsuarioFront();
                    Usuario usu = front.BuscarUsuarioLogado();
                    Usuario usur = contexto.Usuario.FirstOrDefault(c => c.Id == usu.Id);
                    Uteis uteis = new Uteis();
                    int idimg = noticia.ListImagem.LastOrDefault().Id;

                    Imagem arquivo = uteis.SalvaImgArq(up, noticia.Id, titulo, descricao, idimg.ToString());
                    List<TipoImagem> tipImg = contexto.TipoImagem.Where(c=>c.Id == 1).ToList();

                    arquivo.ListTipoImagem = tipImg;

                    arquivo.UsuarioUnico = usur;

                    noticia.ListImagem.Add(arquivo);
                    contexto.Entry<Noticia>(noticia).State = EntityState.Modified;
                    contexto.SaveChanges();
                    return RedirectToAction("EditarArquivo", "ArquivoNoticia", new { id = id });
                 
                }
                return RedirectToAction("EditarArquivo", "ArquivoNoticia", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }
          [PermissaoFiltro(Roles = "Noticia")]
        public ActionResult EditarArquivo(int id)
        {
            try
            {
                var arq = contexto.Noticia.Include(c => c.ListImagem).Where(c => c.Id == id).FirstOrDefault().ListImagem.Where(c=>c.tipo=="arq").ToList();
                ViewBag.noticia = contexto.Noticia.FirstOrDefault(c => c.Id == id);
                ViewBag.Arquivos = arq;

                return View();
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }


        [PermissaoFiltro(Roles = "Noticia")]
        public ActionResult AlterarArquivo(int id)
        {
            try
            {
                Imagem img = contexto.Imagem.Include(c => c.ListNoticia).FirstOrDefault(c=>c.Id == id);
               
                ViewBag.img = img;
               
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }


        [PermissaoFiltro(Roles = "Noticia")]
        [HttpPost]
        public ActionResult AlterarArquivo(FormCollection form, HttpPostedFileBase up)
        {
            try
            {
                int id = Convert.ToInt32(form["id"].ToString());
                string descricao = form["corpo"].ToString();
                string titulo = form["titulo"].ToString();
                Imagem img = contexto.Imagem.Where(c => c.Id == id).Include(c=>c.ListNoticia).FirstOrDefault();
                 int idNoticia =  img.ListNoticia.FirstOrDefault().Id;
               
                if(img !=null)
                {
                    img.Nome = titulo;
                    img.Descricao = descricao;
                    contexto.Entry<Imagem>(img).State = EntityState.Modified;
                    contexto.SaveChanges();
                }

                return RedirectToAction("EditarArquivo", "ArquivoNoticia", new { id = idNoticia });
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }

        [PermissaoFiltro(Roles = "Noticia")]
        public ActionResult ExcluirArquivo(int id)
        {
            try
            {
                Imagem img = contexto.Imagem.Include(c => c.ListNoticia).FirstOrDefault(c => c.Id == id);
                int idNoticia = img.ListNoticia.FirstOrDefault().Id;

                Uteis uteis = new Uteis();
                bool ok =   uteis.ExcluirArqImagem(img.Caminho);
                if (ok == true)
                {
                    contexto.Imagem.Remove(img);
                    contexto.SaveChanges();

                }
                return RedirectToAction("EditarArquivo", "ArquivoNoticia", new { id = idNoticia });
                
              
            }
            catch
            {
                return RedirectToAction("Index", "ArquivoNoticia");
            }

        }
    }
}