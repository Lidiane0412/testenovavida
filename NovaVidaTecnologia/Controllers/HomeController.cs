using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NovaVidaTecnologia.Models;

namespace NovaVidaTecnologia.Controllers
{
    public class HomeController : Controller
    {
        private NovaVidaContext novaVidaContext;
        private readonly IConfiguration _config;
        public HomeController(NovaVidaContext nvc, IConfiguration config)
        {
            novaVidaContext = nvc;
            _config = config;
        }
        public IActionResult Index()
        {


            return View(novaVidaContext.Professores);
        }

        public IActionResult InsertTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertTeacher(Professores professores)
        {
            if (ModelState.IsValid)
            {
                if (novaVidaContext.Professores.Any(p => p.Nome == professores.Nome))
                {

                    TempData["Insuccess"] = false;
                    return View();
                }

                else
                {
                    novaVidaContext.Professores.Add(professores);
                    novaVidaContext.SaveChanges();
                    TempData["success"] = true;

                }

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
