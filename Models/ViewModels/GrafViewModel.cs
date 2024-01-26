using System.Globalization;

namespace WebAppPPA.Models.ViewModels
{
    public class GrafViewModel
    {
        public double[] NumeriVotati { get; set; }   //in percentuale

        public GrafViewModel(double[] numeriVotati )
        {
            NumeriVotati = numeriVotati;
        }
    }
}