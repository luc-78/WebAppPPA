using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppPPA.Models;
using WebAppPPA.Models.InputModels;
using WebAppPPA.Models.Services.Application;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonaService personaService;
        public HomeController(IPersonaService personaService)
        {
            this.personaService = personaService;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Elenco delle persone";
            List<PersonaViewModel> persone = await personaService.GetPersoneAsync();
            return View(persone);
        }

        public async Task<IActionResult> Dettagli(int id)
        {
            PersonaViewModel persona = await personaService.GetPersonaAsync(id);
            ViewData["Title"] = "ID = " + persona.PersonaID;
            return View(persona);
         
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Nuova persona";
            var inputModel = new PersonaCreateInputModel();
            return View(inputModel);
        }
        
        [HttpPost]
        public IActionResult Create(PersonaCreateInputModel inputModel)
        {
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
