using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaVidaTecnologia.Models;

namespace NovaVidaTecnologia.Controllers
{
    public class AlunosController : Controller
    {
        private readonly NovaVidaContext _novaVidaContext;
        //private readonly IFileProvider fileProvider;

        public AlunosController(NovaVidaContext novaVidaContext)
        {

            _novaVidaContext = novaVidaContext;
        }

        public IActionResult Index(int id)
        {
            var data = _novaVidaContext.Alunos.Include(x => x.Professores).Where(x => x.ProfessorId == id).ToArray();
            ViewBag.IdProfessor = id;

            return View(data);
        }

        public async Task<IActionResult> DeleteStudent(int id, int ProfessorId)
        {
            // var data = _context.Student.Where(x => x.Id == id).FirstOrDefault<Student>();
            try
            {
                Alunos alunoDelete = new Alunos() { Id = id };

                _novaVidaContext.Entry(alunoDelete).State = EntityState.Deleted;
                await _novaVidaContext.SaveChangesAsync();
                TempData["Deletesuccess"] = true;


                return RedirectToAction(nameof(Index), new { id = ProfessorId });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult UploadFile()
        {
            return View();
        }
    }
}
