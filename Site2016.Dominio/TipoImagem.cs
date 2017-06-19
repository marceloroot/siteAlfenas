using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.Dominio
{
    public class TipoImagem
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Nome do tipo de arquivo Obrigatorio")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }

        public List<Imagem> ListImagem { get; set; }
        
    }
}
