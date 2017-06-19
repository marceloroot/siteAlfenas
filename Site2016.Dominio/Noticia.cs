using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Site2016.Dominio
{
    public class Noticia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo da Noticia Obrigatorio")]
        [MaxLength(250)]
        public string Titulo{get;set;}

        
     
        public string Corpo{get;set;}

       

        [Required(ErrorMessage = "Data da Noticia Obrigatorio")]
        public DateTime DataPublicacao{get;set;}

        public Usuario UsuarioUnico { get; set; }
        public List<Imagem> ListImagem { get; set; }
        public TipoNoticia TipoNoticiaUnica { get; set; }
        
        
    }
}
