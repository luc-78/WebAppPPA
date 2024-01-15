using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppPPA.Models.InputModels;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IPersonaService
    {
        Task<List<PersonaViewModel>> GetPersoneAsync();
        Task<PersonaViewModel> GetPersonaAsync(int id);
        Task<int> CreaPersonaAsync(PersonaCreateInputModel inputModel);

        //Task<PersonaDetailViewModel> CreateCourseAsync(PersonaCreateInputModel inputModel);
    }
}