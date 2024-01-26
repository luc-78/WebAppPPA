using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

using WebAppPPA;
using WebAppPPA.Models.InputModels;

namespace WebAppPPA.Customizations.ModelBinders
{
    public class HomeInputModelBinder : IModelBinder
    {
        
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Recuperiamo i valori grazie ai value provider
            int.TryParse(bindingContext.ValueProvider.GetValue("Page").FirstValue, out int page);
            
            int perPage=Startup.Configuration.GetSection("Persone").GetValue<int>("PerPage");
            //Creiamo l'istanza del homeInputModel
            var inputModel = new HomeInputModel( page, perPage);

            //Impostiamo il risultato per notificare che la creazione è avvenuta con successo
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            //Restituiamo un task completato
            return Task.CompletedTask;
        }
    }
}