using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAppPPA.Models.Entities;

namespace WebAppPPA.Models.InputModels
{
    public class PersonaModificaInputModel 
    {
         [Required]
        public int PersonaID { get; set; }
        
        [Required(ErrorMessage = "Il Nome è obbligatorio"),
         Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Il Cognome è obbligatorio"),
         Display(Name = "Cognome")] 
        public string Cognome { get; set; }
        
        [Required()]
        public string Data_di_nascita { get; set; }
        
         [Required()]
         public string Telefono { get; set; }
        
         [Required()]
         public string Email { get; set; }


        public static PersonaModificaInputModel FromEntity(Persona persona)
        {
            return new PersonaModificaInputModel {
                PersonaID=persona.PersonaID,
                Nome=persona.Nome,
                Cognome=persona.Cognome,
                Data_di_nascita=persona.Data_di_nascita,
                Telefono=persona.Telefono,
                Email=persona.Email
            };
        }

        
    }
}