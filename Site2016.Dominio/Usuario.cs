using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do usuario Obrigatorio")]
        [MaxLength(150)]
        public string Nome { get; set; }


        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Email Obrigatorio")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Obrigatorio")]
        [MaxLength(200)]
        public string Senha { get; set; }

        public List<Permissao> ListPermissoes { get; set; }
        public List<Noticia> ListNoticia { get; set; }
        public Secretaria SecretariaUnica { get; set; }
        public List<Jari> ListJari { get; set; }
        public List<Imagem> ListImagem { get; set; }

     
    }
}
