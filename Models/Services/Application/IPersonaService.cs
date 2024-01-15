using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IPersonaService
    {
        Task<List<PersonaViewModel>> GetPersoneAsync();
        Task<PersonaViewModel> GetPersonaAsync(int id);

        //Task<PersonaDetailViewModel> CreateCourseAsync(PersonaCreateInputModel inputModel);
    }
}