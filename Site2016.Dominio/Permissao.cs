using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
   public class Permissao
    {
       [Key]
       public int Id { get; set; }

       [Index(IsUnique = true)]
       [Required(ErrorMessage = "Nome da permissao Obrigatorio")]
       [MaxLength(150)]
       public string Nome { get; set; }

       public List<Usuario> ListUsuario { get; set; }
   
    }
}
