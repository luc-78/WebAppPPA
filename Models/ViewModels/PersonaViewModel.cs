using System;
using System.Data;
using WebAppPPA.Models.Entities;
//using MyCourse.Models.Enums;
//using WebAppPPA.Models.ValueObjects;

namespace WebAppPPA.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int PersonaID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Data_di_nascita { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }


        public static PersonaViewModel FromDataRow(DataRow courseRow)
        {
            var personaViewModel = new PersonaViewModel {
               /* Title = Convert.ToString(courseRow["Title"]),
                ImagePath = Convert.ToString(courseRow["ImagePath"]),
                Author = Convert.ToString(courseRow["Author"]),
                Rating = Convert.ToDouble(courseRow["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["FullPrice_Currency"])),
                    Convert.ToDecimal(courseRow["FullPrice_Amount"])
                ),
                CurrentPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["CurrentPrice_Currency"])),
                    Convert.ToDecimal(courseRow["CurrentPrice_Amount"])
                ),
                Id = Convert.ToInt32(courseRow["Id"])*/
            };
            return personaViewModel;
        }

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