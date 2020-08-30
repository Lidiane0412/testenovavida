using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaVidaTecnologia.Models
{
    public class Alunos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal PagamentoMensal { get; set; }
        public DateTime DataVencimento { get; set; }

        public DateTime CriadoEm { get; set; }

        [Required]
        public int ProfessorId { get; set; }

        [ForeignKey("ProfessorId")]
        public Professores Professores { get; set; }
    }
}