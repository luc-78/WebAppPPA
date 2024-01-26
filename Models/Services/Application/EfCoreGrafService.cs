using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppPPA.Models.Services.Infrastructure;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{

    public class EfCoreGrafService : IGrafService
    {
        private readonly WebAppPPADbContext dbContext;

        public EfCoreGrafService(WebAppPPADbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<GrafViewModel> GetNumeriVotatiAsync()
        {
            //prendo il numeri dal db
            IQueryable<int> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Select(persona=>persona.Numero_preferito);

            var num = await queryLinq.ToArrayAsync();
            
            //li raggruppo 10 a 10
            int grandGruppi = 10;
            var gruppi = RaggruppaInt(num, grandGruppi);              //todo da mettere il numero in setting

            //calcolo %, e le metto in un array int[]
            
            int totNum = queryLinq.Count();
            var percentuali = new double[gruppi.Length];
            for (int k =0; k<gruppi.Length; k++)
                {
                    double percento = ( gruppi[k] / (double)totNum) * 100;
                    percentuali[k] = Math.Round(percento);
                }

;
            //return viewModel;
            GrafViewModel gr = new GrafViewModel(percentuali);
            return gr;
        }

        private int[] RaggruppaInt(int[] array, int grandGruppi)
        {
    // Creare una lista per contenere i risultati
            List<int> numeri = array.ToList();

    // Raggruppare i numeri per decine e ordinare per chiave
            var raggruppamentoPerDecine = numeri
                .GroupBy(n => n / grandGruppi) //10
                .OrderBy(g => g.Key);

            int numGruppi = 100/grandGruppi;                //todo da approsimare
            var result = new int[numGruppi]; //10

    // Iterare attraverso tutte le decine da 0 a 9
            for (int decina = 0; decina < numGruppi; decina++)
            {
        // Trovare il gruppo corrispondente, se presente
                var gruppo = raggruppamentoPerDecine.FirstOrDefault(g => g.Key == decina);

        // Se il gruppo esiste, calcolare il conteggio
                if (gruppo != null)
                {
                    result[decina] = gruppo.Count();
                }
                else
                {
                    result[decina] = 0;
                }
            }
            return result;
        }
    }
}