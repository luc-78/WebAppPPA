namespace WebAppPPA.Models.ViewModels
{
    public class GrafEtaViewModel
    {
        public double[] Eta { get; set; }   //in percentuale

        public GrafEtaViewModel(double[] eta )
        {
            Eta = eta;
        }
    }
}