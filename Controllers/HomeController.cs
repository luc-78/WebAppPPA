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

         [HttpGet]
        public async Task<IActionResult>Modifica(int id)
        {
            PersonaModificaInputModel InputModels = await personaService.GetPersonaPerModificaAsync(id);
            ViewData["Title"] = "Modifica Dati";
            return View(InputModels);
         
        }

         [HttpPost]
        public async Task<IActionResult>Modifica(PersonaModificaInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                
                bool risultato = await personaService.ModificaPersonaAsync(inputModel); 
                TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                return RedirectToAction(nameof(Index));
              
            }

            ViewData["Title"] = "Modifica dati";
            return View(inputModel);
         
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Nuova persona";
            var inputModel = new PersonaCreateInputModel();
            return View(inputModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(PersonaCreateInputModel inputModel)
        {
            int personaID = await personaService.CreaPersonaAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PersonaDeleteInputModel inputModel)
        {
            await personaService.DeletePersonaAsync(inputModel);
            TempData["ConfirmationMessage"] = "La persona è stata eliminata";
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
