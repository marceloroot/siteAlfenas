using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Site2016.Dominio
{
  public class Arquivo
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage="Nome do Arquivo Obrigatorio")]
        [MaxLength(150)]
        public string Nome { get; set; }

     
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Caminho do Arquivo Obrigatorio")]
        [MaxLength(200)]
        public string Caminho { get; set; }


        public List<Jari> JariUnico{ get; set; }
        public List<TipoArquivo> ListTipoArquivo { get; set; }

   
  
     
    }
}
