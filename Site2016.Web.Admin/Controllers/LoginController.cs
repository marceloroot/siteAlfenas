using Site2016.Dominio;
using Site2016.InfraEstrutura;
using Site2016.Web.Admin.Models;
using Site2016.Web.Admin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Site2016.Web.Admin.Controllers
{
    public class LoginController : Controller
    {
        protected readonly AppContexto contexto = new AppContexto();
        // GET: Login
        public ActionResult Index()
        {
           
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            try
            {
                UsuarioFront usuarioNegocio = new UsuarioFront();
                Uteis uteis = new Uteis();
                string email = form["email"];
                string senha = form["senha"];
                senha = uteis.Encrypt(senha);
                bool ok =    usuarioNegocio.AutenticarUsuario(email, senha);
                if(ok == true)
                {
                    Usuario usu = usuarioNegocio.BuscarUsuarioLogado();
                    usu = contexto.Usuario.Include(c=>c.ListPermissoes).Where(c => c.Email == email).FirstOrDefault();
                    List<Permissao> listaPermissão = usu.ListPermissoes.ToList();
                    foreach(var variavel in listaPermissão)
                    {
                        if(variavel.Nome == "Noticia")
                        {

                            return RedirectToAction("Index", "Home");
                        }
                        else if(variavel.Nome == "Root")
                        {

                            return RedirectToAction("CriarUsuario", "Login");
                        }
                        else if (variavel.Nome == "Jari")
                        {

                            return RedirectToAction("Index", "Jari");
                        }
                        else if (variavel.Nome == "Secretaria")
                        {

                            return RedirectToAction("Index", "Secretaria");
                        }
                        else if (variavel.Nome == "Telefones")
                        {

                            return RedirectToAction("Index", "Telefone");
                        }


                    }


                }
                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return View();
            }
        }


        
        public ActionResult Deslogar()
        {
            UsuarioFront usuarioNegocio = new UsuarioFront();
            // Usuario usuario = contexto.Usuario.Include(c => c.ListPermissoes).Where(c => c.Email == "marcelo@alfenas.mg.gov.br").FirstOrDefault();
            usuarioNegocio.Deslogar();
            return RedirectToAction("Index", "Login");
        }


        [PermissaoFiltro(Roles = "Root")]
        public ActionResult CriarUsuario()
        {
            ViewBag.permissao = contexto.Permissao.OrderBy(c => c.Nome).ToList();
            return View();
        }


       [PermissaoFiltro(Roles = "Root")]
        [HttpPost]
        public ActionResult CriarUsuario(FormCollection form)
        {
            try
            {

                Uteis uteis = new Uteis();
                List<Permissao> permissao = new List<Permissao>();
             
                string nome = form["nome"];
                string email = form["email"];
                string senha = uteis.Encrypt(form["senha"]);
                int idPermissao =Convert.ToInt32(form["permissao"]);
                permissao = contexto.Permissao.Where(c => c.Id == idPermissao).ToList();
                Usuario usuario = new Usuario();
                usuario.Nome = nome;
                usuario.Email = email;
                usuario.Senha = senha;
                usuario.ListPermissoes = permissao;

                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();

                return RedirectToAction("Index", "Login");
            }
            catch
            {
                ViewBag.erro = "Não foi possivel cadastrar";
                return View();

            }
        }
    }
}