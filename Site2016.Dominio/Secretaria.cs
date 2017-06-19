using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
   public class Secretaria
    {
       [Key]
       public int Id { get; set; }

       [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
       [MaxLength(250)]
       public string Nome { get; set; }

       [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
       public string Atribuicao { get; set; }

       [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
       [MaxLength(150)]
       public string NomeSecretario { get; set; }

       [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
       [MaxLength(300)]
       public string Endereco { get; set; }

       [Required(ErrorMessage = "Descricao do Jari Obrigatorio")]
       [MaxLength(300)]
       public string Horario { get; set; }
       public List<Usuario> ListUsuario { get; set; }

       public List<Telefone> ListTelefone { get; set; }

       public Secretaria SecretariaUnica { get; set; }

       public List<Secretaria> LsitaSubSecretarias { get; set; }

       
    }
}
