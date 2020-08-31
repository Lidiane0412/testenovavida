using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NovaVidaTecnologia.Models;

namespace NovaVidaTecnologia.Controllers
{
    public class FileImportController : Controller
    {
        [Obsolete]
        IHostingEnvironment _appEnvironment;
        private NovaVidaContext _context;
        private readonly IConfiguration _config;

        [Obsolete]
        public FileImportController(IHostingEnvironment env, NovaVidaContext context, IConfiguration config)
        {
            _appEnvironment = env;
            _context = context;
            _config = config;


        }

        public IActionResult Index(int id)
        {

            ViewBag.IdImport = id;
            return View(_context.Professores);
        }


        [HttpPost]
        [Obsolete]
        public IActionResult Index(IFormFile fileImport, int teacher_id)
        {
            bool Import = false;
            var tableData = _context.LogImportacao.CountAsync();

            if (tableData.Result == 0)
            {
                Import = true;
            }

            else
            {
                var data = _context.LogImportacao.MaxAsync(x => x.DataImportacao);
                DateTime dataH = Convert.ToDateTime(data.Result.ToString());
                var minInterval = int.Parse(_config.GetSection("TimeImport:IntervalMinutes").Value);

                var hourLastImport = dataH;
                var hourLastImportnterval = hourLastImport.AddMinutes(minInterval);

                var dateNow = DateTime.Now;
                if (dateNow >= hourLastImportnterval)
                {
                    Import = true;
                }
                else
                {
                    Import = false;
                }
            }



            // aqui pode importar
            if (Import)
            {
                ViewBag.Import = ViewBag.IdImport;
                if (fileImport == null || fileImport.Length == 0)
                {
                    ViewData["Erro"] = "Error: Arquivo não selecionado.";
                    return View(ViewData);
                }

                if (!fileImport.FileName.Contains(".txt"))
                {
                    ViewData["Erro"] = "Error: Formato do arquivo incorreto.";
                    return View(ViewData);
                }

                string fileName = "import_" + fileImport.FileName;
                //string fileName = "import_" + Hash.getHash() + ".txt";

                string pathFileComplete = Path.Combine(_appEnvironment.WebRootPath, "files", fileName);

                using (var stream = new FileStream(pathFileComplete, FileMode.Create))
                {
                    fileImport.CopyTo(stream);
                }

                var lines = System.IO.File.ReadAllLines(pathFileComplete);


                foreach (string line in lines)
                {
                    var lineData = line.Split("||");

                    var lineStudent = new Alunos();

                    lineStudent.Nome = lineData[0];
                    lineStudent.PagamentoMensal = decimal.Parse(lineData[1]);
                    lineStudent.DataVencimento = DateTime.Parse(lineData[2]);
                    lineStudent.ProfessorId = teacher_id;
                    _context.Alunos.Add(lineStudent);
                    _context.SaveChanges();


                }
                TempData["success"] = true;

                var dataImport = new LogImportacao();
                dataImport.NomeArquivo = fileImport.FileName;
                //dataImport.DateImport = DateTime.Now;
                _context.LogImportacao.Add(dataImport);
                _ = _context.SaveChanges();
            }
            else
            { // aqui é quando não pode importar
                TempData["TimeShort"] = true;
            }




            return RedirectToAction("Index", "Alunos", new { id = teacher_id });
        }

        private string getHash()
        {
            throw new NotImplementedException();
        }
    }
}
