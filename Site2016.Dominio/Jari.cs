using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
    public class Jari
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
        [MaxLength(150)]
        public string Descricao{get;set;}

        [Required(ErrorMessage = "Data do Jari Obrigatorio")]
        public DateTime DataPublicacao { get; set; }

        public TipoJari TipoJariUnico { get; set; }

        public Arquivo ArquivoUnico { get; set; }
        public Usuario UsuarioUnico { get; set; }
       
    }
}
