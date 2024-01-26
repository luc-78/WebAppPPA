using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebAppPPA.Models.Entities;
using WebAppPPA.Models.InputModels;
using WebAppPPA.Models.Services.Infrastructure;
using WebAppPPA.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace WebAppPPA.Models.Services.Application
{
    public class EfCorePersonaService : IPersonaService
    {
        private readonly WebAppPPADbContext dbContext;

        public EfCorePersonaService(WebAppPPADbContext dbContext)
        {
            this.dbContext = dbContext;
        }

          public async Task<ListViewModel<PersonaViewModel>> GetPersoneAsync(HomeInputModel input)
        {
            //int persPerPage = Startup.Configuration.GetSection("Persone").GetValue<int>("PerPage");
            int page=input.Page;
            int limit=input.Limit;
            int offset=input.Offset;

            IQueryable<PersonaViewModel> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Select(persona => PersonaViewModel.FromEntity(persona)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            List<PersonaViewModel> persone = await queryLinq
                .Skip(offset)
                .Take(limit)
                .ToListAsync(); //La query al database viene inviata qui, quando manifestiamo l'intenzione di voler leggere i risultati

            int totalCount= await queryLinq
                .CountAsync();

            ListViewModel<PersonaViewModel> result = new ListViewModel<PersonaViewModel>
            {
                Results = persone,
                TotalCount = totalCount
            };

            return result;
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
                                        inputModel.Telefono,
                                        inputModel.Numero_preferito);
            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();
            await InviaMailBenvenutoAsync(persona);
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
            persona.Numero_preferito=inputModel.Numero_preferito;
            
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

        public Task InviaMailBenvenutoAsync(Persona persona)
        {
            var emailSender= new EmailSender();

            // Invio la domanda
            try
            {
                emailSender.SendEmail(persona.Email,
                                        $"{persona.Nome} {persona.Cognome}",
                                        $"Benvenuto {persona.Nome} {persona.Cognome}",
                                        "Benvenuto in ProgettoPA!");
            }
            catch 
            {
                //throw new SendException();
            }

            return Task.CompletedTask;
        }
    }
}