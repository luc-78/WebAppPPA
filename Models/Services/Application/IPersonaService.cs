using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppPPA.Models.InputModels;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IPersonaService
    {
        Task<List<PersonaViewModel>> GetPersoneAsync(int page);
        Task<PersonaViewModel> GetPersonaAsync(int id);
        Task<int> CreaPersonaAsync(PersonaCreateInputModel inputModel);
        Task<PersonaModificaInputModel> GetPersonaPerModificaAsync(int id);
        Task<Boolean> ModificaPersonaAsync(PersonaModificaInputModel inputModel);
        Task DeletePersonaAsync(PersonaDeleteInputModel inputModel);
    }
}