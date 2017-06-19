using Site2016.Dominio;
using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Site2016.Web.Admin.Security;

namespace Site2016.Web.Admin.Controllers
{

    public class SecretariaController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Secretaria
        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult Index()
        {

            return View();
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form)
        {
               
                Secretaria secretaria = new Secretaria();
                secretaria.Nome = form["nome"].ToString();
                secretaria.NomeSecretario = form["secretario"].ToString();
                secretaria.Endereco = form["endereco"].ToString();
                secretaria.Horario = form["func"].ToString();
                secretaria.Atribuicao = form["Descricao"].ToString();
                secretaria.SecretariaUnica = null;
                contexto.Secretaria.Add(secretaria);
                contexto.SaveChanges();

               

               // ViewBag.Secretaria = secretarias;
                return RedirectToAction("AlterarSecretarias", "Secretaria");
            //}
            //catch
            //{
            //    List<Secretaria> secretarias = new List<Secretaria>();
            //    secretarias = contexto.Secretaria.ToList();
            //    ViewBag.Secretaria = secretarias;
            //    return RedirectToAction("AlterarSecretarias", "Secretaria");

            //}

        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult AlterarSecretarias()
        {
            try
            {

                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;

                return View();
            }
            catch
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;
                return View();
            }
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult PaginaAlterarSecretaria(int IdSecretaria)
        {
            Secretaria secretaria = new Secretaria();
            secretaria = contexto.Secretaria.Where(c => c.Id == IdSecretaria).FirstOrDefault();
            ViewBag.Secretaria = secretaria;

            return View();
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PaginaAlterarSecretaria(FormCollection form)
        {
            try
            {
                int idSecretaria = Convert.ToInt32(form["Id"]);
                Usuario usuario = contexto.Usuario.FirstOrDefault();
                string erro = "";
                Secretaria secretaria = contexto.Secretaria.Where(c => c.Id == idSecretaria).FirstOrDefault();


                secretaria.Nome = form["nome"];
                secretaria.NomeSecretario = form["secretario"];
                secretaria.Endereco = form["endereco"];
                secretaria.Horario = form["func"];
                secretaria.Atribuicao = form["Descricao"];



                contexto.Entry<Secretaria>(secretaria).State = System.Data.Entity.EntityState.Modified;

                contexto.SaveChanges();

                ViewBag.erro = erro;
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;
                return RedirectToAction("AlterarSecretarias", "Secretaria");

            }
            catch (Exception ex)
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;
                ViewBag.erro = ex.Message.ToString();
                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }

        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult Excluir(int idSecretaria)
        {
            try
            {

                Secretaria secretaria = contexto.Secretaria.Where(c => c.Id == idSecretaria).FirstOrDefault();
                contexto.Secretaria.Remove(secretaria);
                contexto.SaveChanges();
                List<Secretaria> secretarias = new List<Secretaria>();
                AppContexto contextonovo = new AppContexto();
                secretarias = contextonovo.Secretaria.ToList();
                contextonovo.Dispose();
                ViewBag.Secretaria = secretarias;

                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
            catch
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;
                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult VincularSecretaria(int idSecretaria)
        {
            try
            {

                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = contexto.Secretaria.Where(c => c.Id == idSecretaria).FirstOrDefault();
                ViewBag.Secretarias = secretarias;
                return View();
            }
            catch
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();

                ViewBag.Secretarias = secretarias;
                return View();
            }
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult FinalizarVinculo(int idSubSecretaria, int idSecretaria)
        {
            try
            {
                Secretaria subSecretaria = new Secretaria();
                Secretaria secretaria = new Secretaria();
                subSecretaria = contexto.Secretaria.Where(c => c.Id == idSubSecretaria).FirstOrDefault();
                secretaria = contexto.Secretaria.Include(c => c.LsitaSubSecretarias).Where(c => c.Id == idSecretaria).FirstOrDefault();
                subSecretaria.SecretariaUnica = secretaria;

                contexto.Entry<Secretaria>(subSecretaria).State = EntityState.Modified;
                contexto.SaveChanges();


                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;

                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
            catch
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;

                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult RemoverVinculo(int idSubSecretaria)
        {
            try
            {
                Secretaria subSecretaria = new Secretaria();

                subSecretaria = contexto.Secretaria.Include(c => c.SecretariaUnica).Where(c => c.Id == idSubSecretaria).FirstOrDefault();

                subSecretaria.SecretariaUnica = null;

                contexto.Entry<Secretaria>(subSecretaria).State = EntityState.Modified;
                contexto.SaveChanges();


                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;

                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
            catch
            {
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.ToList();
                ViewBag.Secretaria = secretarias;

                return RedirectToAction("AlterarSecretarias", "Secretaria");
            }
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult EditarTelefone(int idSecretaria)
        {
            Secretaria secretaria = new Secretaria();
            secretaria = contexto.Secretaria.Where(c => c.Id == idSecretaria).Include(c => c.ListTelefone).FirstOrDefault();
            ViewBag.Secretaria = secretaria;
            ViewBag.Telefone = secretaria.ListTelefone;
            return View();
        }

        [PermissaoFiltro(Roles = "Secretaria")]
        public ActionResult DestacarTel(int idTel, int idSec)
        {
            try
            {
                Secretaria secretaria = new Secretaria();
                secretaria = contexto.Secretaria.Include(c => c.ListTelefone).Where(c => c.Id == idSec).FirstOrDefault();
                var validar = secretaria.ListTelefone.Where(c => c.destaque == true).FirstOrDefault();
                if (validar != null)
                {
                    secretaria.ListTelefone.Where(c => c.destaque == true).FirstOrDefault().destaque = false;
                }
                secretaria.ListTelefone.Where(c => c.Id == idTel).FirstOrDefault().destaque = true;
                contexto.Entry<Secretaria>(secretaria).State = EntityState.Modified;
                contexto.SaveChanges();


                return RedirectToAction("EditarTelefone", "Secretaria", new { idSecretaria = idSec });
            }
            catch
            {

                return RedirectToAction("EditarTelefone", "Secretaria", new { idSecretaria = idSec });
            }
        }

    }
}