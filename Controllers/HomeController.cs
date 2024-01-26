using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        
        private static string ViewBagToastScript = "";

        public async Task<IActionResult> Index(HomeInputModel input)
        {
            ViewData["Title"] = "Elenco delle persone";
            ListViewModel<PersonaViewModel> persone = await personaService.GetPersoneAsync(input);
            PersonaListViewModel viewModel = new PersonaListViewModel
            {
                Persone = persone,
                Input = input
            };


            if(ViewBagToastScript != "")
                {
                    ViewBag.ToastScript =  $"mostraToastConferma('{ViewBagToastScript}');";
                    ViewBagToastScript="";
                }
           
            return View(viewModel);
        }

         [HttpGet]
        public async Task<IActionResult>Modifica(int id)
        {
            PersonaModificaInputModel InputModels = await personaService.GetPersonaPerModificaAsync(id);
            ViewData["Title"] = "Modifica Dati: " + InputModels.Nome + " " + InputModels.Cognome;
            return View(InputModels);
         
        }

         [HttpPost]
        public async Task<IActionResult>Modifica(PersonaModificaInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                
                bool risultato = await personaService.ModificaPersonaAsync(inputModel); 
                //ViewBag.ToastScript = "mostraToastConferma();";//TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                ViewBagToastScript="modifiche effettuate";
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
            //ViewBag.ToastScript = "mostraToastConferma();"
            ViewBagToastScript="persona aggiunta";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PersonaDeleteInputModel inputModel)
        {
            await personaService.DeletePersonaAsync(inputModel);
            ViewBagToastScript="La persona è stata eliminata";
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
