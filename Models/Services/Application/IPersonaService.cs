using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IPersonaService
    {
         Task<List<PersonaViewModel>> GetPersoneAsync();
         
         //Task<CourseDetailViewModel> GetPersonaAsync(int id);
    }
}