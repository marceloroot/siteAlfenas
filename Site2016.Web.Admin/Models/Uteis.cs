using Site2016.Dominio;
using Site2016.InfraEstrutura;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Site2016.Web.Admin.Models
{
    public class Uteis
    {
        protected readonly AppContexto contexto = new AppContexto();
        public Int64 GerarID()
        {
            try
            {
                DateTime data = new DateTime();
                data = DateTime.Now;
                string s = data.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                return Convert.ToInt64(s);
            }
            catch (Exception erro)
            {

                throw new Exception("Erro ao gerar id: " + erro.Message.ToString());
            }
        }

        public List<Imagem> SalvaImagens(List<HttpPostedFileBase> Arquvios, int id, int TipoIMGA)
        {
            #region salva Imagens
            List<Imagem> imagens = new List<Imagem>();
            int conterro = 0;
            string erro = "";
            int arquivosSalvos = TipoIMGA;
            if (Arquvios.Count > 0)
            {
                try
                {

                    foreach (var file in Arquvios)
                    {
                        //1388608

                        var name = file.ContentType;
                        if (file.ContentLength > 1388608)
                        {
                            if (conterro == 0)
                            {
                                erro = "Os arquivos a seguir possui mais de 1 MB e não foi salvo: ";
                            }
                            erro = erro + file.FileName + ", ";
                            conterro++;

                        }
                        else
                        {
                            if (file.ContentType == "image/jpeg" ||
                                         file.ContentType == "image/png" ||
                                         file.ContentType == "image/gif" ||
                                         file.ContentType == "image/bmp")
                            {
                                if (file.ContentLength > 0)
                                {
                                    Imagem img = new Imagem();

                                    //Pega o nome do arquivo
                                    string nome = System.IO.Path.GetFileName(file.FileName);
                                    //Pega a extensão do arquivo
                                    string extensao = Path.GetExtension(file.FileName);
                                    //Gera nome novo do Arquivo numericamente
                                    string filename = id.ToString();
                                    //Caminho a onde será salvo

                                    string nomeSalvar = filename + arquivosSalvos + extensao;
                                    var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/Img/"));
                                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(nomeSalvar));

                                    file.SaveAs(caminhoArquivo);
                                    arquivosSalvos++;

                                    img.Nome = nomeSalvar;
                                    img.Caminho = nomeSalvar;
                                    img.Descricao = nomeSalvar;


                                    imagens.Add(img);
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Cadastre pelo menos uma foto");
                }
            }
            #endregion


            return imagens;
        }

        public Arquivo SalvaArqivo(HttpPostedFileBase Arquvio, int id)
        {
            #region salva arquivo
            Arquivo arq = new Arquivo();

            string erro = "";
            if (Arquvio != null)
            {
                try
                {


                    //1388608

                    var name = Arquvio.ContentType;
                    if (Arquvio.ContentLength > 4388608)
                    {

                        erro = "Os arquivos a seguir possui mais de 1 MB e não foi salvo: ";


                    }
                    else
                    {
                        if (Arquvio.ContentType == "application/pdf")
                        {
                            if (Arquvio.ContentLength > 0)
                            {


                                //Pega o nome do arquivo
                                string nome = System.IO.Path.GetFileName(Arquvio.FileName);
                                //Pega a extensão do arquivo
                                string extensao = Path.GetExtension(Arquvio.FileName);
                                //Gera nome novo do Arquivo numericamente
                                string filename = id.ToString();
                                //Caminho a onde será salvo

                                string nomeSalvar = filename + extensao;
                                var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/ArqJari/"));
                                string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(nomeSalvar));

                                Arquvio.SaveAs(caminhoArquivo);



                                arq.Nome = nomeSalvar;
                                arq.Caminho = nomeSalvar;
                                arq.Descricao = nomeSalvar;



                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    throw new Exception("Cadastre pelo menos uma foto");
                }
            }
            #endregion
            return arq;


        }

        public List<Imagem> ExcluirListaImagens(List<Imagem> imagens)
        {

            try
            {

                foreach (var variavel in imagens)
                {
                    Imagem img = new Imagem();
                    img = contexto.Imagem.Where(c => c.Id == variavel.Id).FirstOrDefault();
                    Uteis uteis = new Uteis();
                    if (img.tipo != "arq")
                    {
                        bool ok = uteis.ExcluirImagem(img.Caminho);

                        if (ok == true)
                        {

                            contexto.Imagem.Remove(img);
                            contexto.SaveChanges();

                        }
                    }
                    else
                    {
                        bool ok = uteis.ExcluirArqImagem(img.Caminho);

                        if (ok == true)
                        {

                            contexto.Imagem.Remove(img);
                            contexto.SaveChanges();

                        }
                    }
                }
                return null;
            }
            catch
            {
                return imagens;
            }
        }

       

        public bool ExcluirImagem(string file)
        {
            try
            {
                if (file != "")
                {

                    //    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/Img/") + file);


                    var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/Img/"));
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(file));
                    System.IO.File.Delete(caminhoArquivo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public bool ExcluirArquivo(Arquivo arq)
        {
            try
            {
                if (arq != null)
                {
                    AppContexto contextoNovo = new AppContexto();

                    var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/Img/"));
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arq.Caminho));
                    System.IO.File.Delete(caminhoArquivo);
                    //  System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/ArqJari/") + arq.Caminho);
                    Arquivo arn = new Arquivo();
                    arn = contextoNovo.Arquivo.Include(c => c.JariUnico).Include(c => c.ListTipoArquivo).Where(c => c.Id == arq.Id).FirstOrDefault();
                    contextoNovo.Arquivo.Remove(arn);
                    contextoNovo.SaveChanges();
                    contextoNovo.Dispose();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        public string Encrypt(string password)
        {
            password += "|2d331cca-f6c0-40c0-bb43-6e32989c2881";
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(password));
            System.Text.StringBuilder sbString = new System.Text.StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString();
        }

        public bool ExcluirImgArq(Imagem arq)
        {
            try
            {
                if (arq != null)
                {
                    AppContexto contextoNovo = new AppContexto();

                    var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/Img/"));
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arq.Caminho));
                    System.IO.File.Delete(caminhoArquivo);
                    //  System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/ArqJari/") + arq.Caminho);
                    Arquivo arn = new Arquivo();
                    arn = contextoNovo.Arquivo.Include(c => c.JariUnico).Include(c => c.ListTipoArquivo).Where(c => c.Id == arq.Id).FirstOrDefault();
                    contextoNovo.Arquivo.Remove(arn);
                    contextoNovo.SaveChanges();
                    contextoNovo.Dispose();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        public Imagem SalvaImgArq(HttpPostedFileBase Arquvio, int id, string titulo, string desrcicao, string idmg)
        {
            #region salva img
            Imagem arq = new Imagem();

            string erro = "";
            if (Arquvio != null)
            {
                try
                {


                    //1388608

                    var name = Arquvio.ContentType;
                    if (Arquvio.ContentLength > 4388608)
                    {

                        erro = "Os arquivos a seguir possui mais de 1 MB e não foi salvo: ";


                    }
                    else
                    {
                        if (Arquvio.ContentType == "application/pdf")
                        {
                            if (Arquvio.ContentLength > 0)
                            {


                                //Pega o nome do arquivo
                                string nome = System.IO.Path.GetFileName(Arquvio.FileName);
                                //Pega a extensão do arquivo
                                string extensao = Path.GetExtension(Arquvio.FileName);
                                //Gera nome novo do Arquivo numericamente
                                string filename = id.ToString();
                                //Caminho a onde será salvo

                                string nomeSalvar = filename + idmg + extensao;
                                var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/NoticiaArq/"));
                                string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(nomeSalvar));

                                Arquvio.SaveAs(caminhoArquivo);



                                arq.Nome = titulo;
                                arq.Caminho = nomeSalvar;
                                arq.Descricao = desrcicao;
                                arq.tipo = "arq";


                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    throw new Exception("Cadastre pelo menos uma foto");
                }
            }
            #endregion
            return arq;


        }


        public bool ExcluirArqImagem(string file)
        {
            try
            {
                if (file != "")
                {

                    //    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/Img/") + file);


                    var uploadPath = (HttpContext.Current.Server.MapPath("~/Uploads/NoticiaArq/"));
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(file));
                    System.IO.File.Delete(caminhoArquivo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}