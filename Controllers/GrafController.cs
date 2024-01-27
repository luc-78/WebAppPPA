using System.Threading.Tasks;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using WebAppPPA.Models.Services.Application;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Controllers
{
    public class GrafController : Controller
    {
        private readonly IGrafService grafService;
        public GrafController(IGrafService grafService)
        {
            this.grafService = grafService;
        }


        public async Task<IActionResult> MostraGraf()
        {
            ViewData["Title"] = "Grafico preferenza numeri";
            GrafViewModel model= await grafService.GetNumeriVotatiAsync();
            return View(model);
        }

        public async Task<IActionResult> MostraEtaGraf()
        {
            ViewData["Title"] = "Grafico età";
            GrafEtaViewModel model= await grafService.GetEtaAsync();
            return View(model);
        }
    }
}        