using Site2016.Dominio;
using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Site2016.Web.Admin.Models;
using Site2016.Web.Admin.Security;

namespace Site2016.Web.Admin.Controllers
{
    public class TelefoneController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Telefone

        [PermissaoFiltro(Roles = "Telefones")]
        public ActionResult Index(int? id)
        {
            Secretaria secretaria = new Secretaria();
            List<Secretaria> secretarias = new List<Secretaria>();
            secretarias = contexto.Secretaria.ToList();
            if (id != null)
                secretaria = contexto.Secretaria.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.sec = secretaria;
            ViewBag.Secretaria = secretarias;
            return View();
        }

        [PermissaoFiltro(Roles = "Telefones")]
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                Secretaria secretaria = new Secretaria();
                int idSec = Convert.ToInt32(form["idsec"]);
                secretaria = contexto.Secretaria.Where(c => c.Id == idSec).FirstOrDefault();
                Telefone telefone = new Telefone();
                telefone.Numero = form["numero"];
                telefone.SecretariaUnica = secretaria;
                telefone.destaque = false;
                contexto.Telefone.Add(telefone);
                contexto.SaveChanges();
                return RedirectToAction("Index", "Telefone");
            }
            catch
            {
                return RedirectToAction("Index", "Telefone");

            }
        }

        [PermissaoFiltro(Roles = "Telefones")]
        public ActionResult LsitaTelefone()
        {
            List<Telefone> telefones = new List<Telefone>();
            telefones = contexto.Telefone.Include(c => c.SecretariaUnica).OrderBy(c => c.SecretariaUnica.Nome).ToList();
            ViewBag.Telefones = telefones.ToArray();
            return View();
        }

        [PermissaoFiltro(Roles = "Telefones")]
        public ActionResult AlterarTelefone(int idTelefone)
        {
            try
            {
                Telefone telefone = new Telefone();
                List<Secretaria> secretarias = new List<Secretaria>();
                secretarias = contexto.Secretaria.OrderBy(c => c.Nome).ToList();
                telefone = contexto.Telefone.Where(c => c.Id == idTelefone).FirstOrDefault();
                ViewBag.Telefone = telefone;
                ViewBag.Secre = secretarias;
                ViewBag.sec = telefone.SecretariaUnica;
                return View();
            }
            catch
            {
                return View();
            }
        }

        [PermissaoFiltro(Roles = "Telefones")]
        [HttpPost]
        public ActionResult AlterarTelefone(FormCollection form)
        {
            try
            {
                Telefone telefone = new Telefone();

                int idSec = Convert.ToInt32(form["idsec"]);
                int idTel = Convert.ToInt32(form["idTel"]);

                Secretaria secretaria = contexto.Secretaria.Where(c => c.Id == idSec).FirstOrDefault();
                telefone = contexto.Telefone.Include(c => c.SecretariaUnica).Where(c => c.Id == idTel).FirstOrDefault();

                telefone.Id = idTel;
                telefone.Numero = form["numero"];
                telefone.SecretariaUnica = secretaria;

                contexto.Entry<Telefone>(telefone).State = EntityState.Modified;
                contexto.SaveChanges();
                return RedirectToAction("LsitaTelefone", "Telefone");
            }
            catch
            {
                return RedirectToAction("LsitaTelefone", "Telefone");
            }
        }

        [PermissaoFiltro(Roles = "Telefones")]
        public ActionResult Excluir(int idTelefone)
        {
            try
            {
                Telefone tel = new Telefone();
                tel = contexto.Telefone.Where(c => c.Id == idTelefone).FirstOrDefault();
                contexto.Telefone.Remove(tel);
                contexto.SaveChanges();


                return RedirectToAction("LsitaTelefone", "Telefone");
            }
            catch
            {

                return RedirectToAction("LsitaTelefone", "Telefone");
            }
        }




    }
}