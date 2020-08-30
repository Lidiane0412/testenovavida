using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaVidaTecnologia.Models
{
    public class Professores
    {
        [Key]
        public int Id { get; set; }

        
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Não é permitido números e caracteres especiais.")]
        public string Nome { get; set; }

        public DateTime CriadoEm { get; set; }

        public ICollection<Alunos> Alunos { get; set; }
    }
}
