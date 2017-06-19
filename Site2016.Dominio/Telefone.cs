using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
   public  class Telefone
    {
      [Key]
      public int Id { get; set; }


      [Required(ErrorMessage = "Numero do telefone Obrigatorio")]
      [MaxLength(100)]
      [Index(IsUnique=true)]
      public string Numero { get; set; }

      public bool destaque { get; set; }

      public Secretaria SecretariaUnica { get; set; }


     
    }
}
