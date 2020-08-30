using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaVidaTecnologia.Models
{
    public class LogImportacao
    {
        [Key]
        public int Id { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataImportacao { get; set; }
    }
}
