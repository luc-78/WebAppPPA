using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebAppPPA.Controllers;

namespace WebAppPPA.Models.InputModels
{
    public class PersonaCreateInputModel
    {
        /*
        [Required(ErrorMessage = "Il titolo è obbligatorio"),
         MinLength(10, ErrorMessage = "Il titolo dev'essere di almeno {1} caratteri"),
         MaxLength(100, ErrorMessage = "Il titolo dev'essere di al massimo {1} caratteri"),
         RegularExpression(@"^[\w\s\.']+$", ErrorMessage = "Titolo non valido"),
         Remote(action: nameof(CoursesController.IsTitleAvailable), controller: "Courses", ErrorMessage = "Il titolo esiste già")]

        public string Title { get; set; }
        */
        public string Nome { get;  set; }
        public string Cognome { get;  set; }
        public string Data_di_nascita { get;  set; }
        public string Telefono { get;  set; }
        public string Email { get;  set; }
    }
}