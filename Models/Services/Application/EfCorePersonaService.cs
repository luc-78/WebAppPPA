using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebAppPPA.Models.Entities;
using WebAppPPA.Models.InputModels;
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

          public async Task<List<PersonaViewModel>> GetPersoneAsync()
        {
            IQueryable<PersonaViewModel> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Select(persona => PersonaViewModel.FromEntity(persona)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            List<PersonaViewModel> persone = await queryLinq.ToListAsync(); //La query al database viene inviata qui, quando manifestiamo l'intenzione di voler leggere i risultati

            return persone;
        }

        public async Task<PersonaViewModel> GetPersonaAsync(int id)
        {
            IQueryable<PersonaViewModel> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Where(persona => persona.PersonaID==id)
                .Select(persona => PersonaViewModel.FromEntity(persona));
            
            PersonaViewModel person = await queryLinq.FirstAsync();
            return person;
           
           
        }

        public async Task<int> CreaPersonaAsync(PersonaCreateInputModel inputModel)
        {
            var persona = new Persona(inputModel.Nome,
                                        inputModel.Cognome,
                                        inputModel.Email,
                                        inputModel.Data_di_nascita,
                                        inputModel.Telefono);
            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();
            return persona.PersonaID;
        }

        public async Task<PersonaModificaInputModel> GetPersonaPerModificaAsync(int id)
        {
            IQueryable<PersonaModificaInputModel> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Where(persona => persona.PersonaID == id)
                .Select(persona => PersonaModificaInputModel.FromEntity(persona)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            PersonaModificaInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            return viewModel;
        }

        public async Task<bool> ModificaPersonaAsync(PersonaModificaInputModel inputModel)
        {
            Persona persona = await dbContext.Persone.FindAsync(inputModel.PersonaID);
            
            if (persona == null)
            {
                //throw new CourseNotFoundException(inputModel.Id);
            }

            persona.Nome=inputModel.Nome;
            persona.Cognome=inputModel.Cognome;
            persona.Data_di_nascita=inputModel.Data_di_nascita;
            persona.Email=inputModel.Email;
            persona.Telefono=inputModel.Telefono;
            
            //dbContext.Update(course);

            try
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException exc) when ((exc.InnerException as SqliteException)?.SqliteErrorCode == 19)
            {
                //throw new ;
                return false;
            }
  
        }

        public async Task DeletePersonaAsync(PersonaDeleteInputModel inputModel)
        {
            Persona persona = await dbContext.Persone.FindAsync(inputModel.PersonaID);
            if (persona == null)
            {
                //throw new 
            }
            dbContext.Remove(persona);
            await dbContext.SaveChangesAsync();
        }
    }
}