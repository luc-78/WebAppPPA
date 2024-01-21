using System;
using System.Data;
using WebAppPPA.Models.Entities;

namespace WebAppPPA.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int PersonaID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Data_di_nascita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }


        public static PersonaViewModel FromEntity(Persona persona)
        {
            return new PersonaViewModel {
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