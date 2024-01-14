using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppPPA.Models.Services.Infrastructure;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public class EfCorePersonaService : IPersonaService
    {
        private readonly WebAppPPADbContext dbContext;

        public EfCorePersonaService(WebAppPPADbContext dbContext)
        {
            this.dbContext = dbContext;
        }

  /*      public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> queryLinq = dbContext.Courses
                .AsNoTracking()
                .Include(course => course.Lessons)
                .Where(course => course.Id == id)
                .Select(course => CourseDetailViewModel.FromEntity(course)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato
            
            CourseDetailViewModel viewModel = await queryLinq.SingleAsync();
                                                           //.FirstOrDefaultAsync(); //Restituisce null se l'elenco è vuoto e non solleva mai un'eccezione
                                                           //.SingleOrDefaultAsync(); //Tollera il fatto che l'elenco sia vuoto e in quel caso restituisce null, oppure se l'elenco contiene più di 1 elemento, solleva un'eccezione
                                                           //.FirstAsync(); //Restituisce il primo elemento, ma se l'elenco è vuoto solleva un'eccezione
                
            return viewModel;
        }
*/
        public async Task<List<PersonaViewModel>> GetPersoneAsync()
        {
            IQueryable<PersonaViewModel> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Select(persona => PersonaViewModel.FromEntity(persona)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            List<PersonaViewModel> persone = await queryLinq.ToListAsync(); //La query al database viene inviata qui, quando manifestiamo l'intenzione di voler leggere i risultati

            return persone;
        }
    }
}