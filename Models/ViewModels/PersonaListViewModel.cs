using System.Collections.Generic;
using WebAppPPA.Models.InputModels;

namespace WebAppPPA.Models.ViewModels
{
    public class PersonaListViewModel
    {
        public ListViewModel<PersonaViewModel> Persone { get; set; }
        public HomeInputModel Input { get; set; }
    }
}