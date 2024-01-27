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

        public async Task<GrafEtaViewModel> GetEtaAsync()
        {
            //prendo le date nascita dal db
            IQueryable<string> queryLinq = dbContext.Persone
                .AsNoTracking()
                .Select(persona=>persona.Data_di_nascita);

            var dataNascita = await queryLinq.ToArrayAsync();

            //prendo le età
            var eta = new int[dataNascita.Length];
            int i = 0;  
            foreach(string data in dataNascita)
                {
                    // Data di nascita in formato "yyyy-mm-dd"
                    // Converti la stringa in un oggetto DateTime
                    DateTime dataDiNascita = DateTime.ParseExact(data, "yyyy-MM-dd", null);

                    // Calcola l'età
                    eta[i] = CalcolaEta(dataDiNascita);
                    i++;
                }
            
            //li raggruppo "1-18","18-30","30-60","60+"
            var gruppi = new int[4];
            foreach(int etaPersona in eta)
                {
                    if (etaPersona < 18)
                    {
                        gruppi[0] = gruppi[0] + 1;
                    }
                    else if (etaPersona >= 18 && etaPersona < 30)
                    {
                        gruppi[1] = gruppi[1] + 1;
                    }
                    else if (etaPersona >= 30 && etaPersona < 60)
                    {
                        gruppi[2] = gruppi[2] + 1;
                    }
                    else
                    {
                        gruppi[3] = gruppi[3] + 1;
                    }
                }
            
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
            GrafEtaViewModel gr = new GrafEtaViewModel(percentuali);
            return gr;
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

        private int CalcolaEta(DateTime dataDiNascita)
        {
            DateTime oggi = DateTime.Today;
            int eta = oggi.Year - dataDiNascita.Year;

            // Riduci l'età di 1 se il compleanno non è ancora passato quest'anno
            if (oggi < dataDiNascita.AddYears(eta))
            {
                eta--;
            }

            return eta;
        }
    }
}