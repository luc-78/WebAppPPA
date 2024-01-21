using System;

namespace WebAppPPA.Models.Services
{
public static class FormatData
{
    public static string CambiaFormatoData(string inputString)
    {
        // Converte la stringa in un oggetto DateTime
        DateTime inputDate = DateTime.ParseExact(inputString, "yyyy-MM-dd", null);

        // Formatta la data come stringa nel formato desiderato
        string outputString = inputDate.ToString("dd/MM/yyyy");

        //  il risultato
        return outputString;
    }
}

    
}