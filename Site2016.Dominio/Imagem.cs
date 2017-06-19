using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
    public class Imagem
    {
         [Key]
         public int Id { get; set; }

      
        [Required(ErrorMessage = "Nome da imagem Obrigatorio")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(400)]
        public string Descricao { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Caminho da Imagem Obrigatorio")]
        [MaxLength(200)]
        public string Caminho { get; set; }

        public string tipo { get; set; }

        [Required(ErrorMessage = "Destaque da Noticia Obrigatorio")]
        public bool Destaque { get; set; }

        public List<Noticia> ListNoticia { get; set; }
        public List<TipoImagem> ListTipoImagem { get; set; }
        public Usuario UsuarioUnico { get; set; }

        
    }
}
