using System.ComponentModel.DataAnnotations;

namespace WebAppPPA.Models.InputModels
{
    public class PersonaDeleteInputModel
    {
        [Required]
        public int PersonaID { get; set; }
    }
}