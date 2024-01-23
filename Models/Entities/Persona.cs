using System;
using System.Collections.Generic;

namespace WebAppPPA.Models.Entities
{
    public partial class Persona
    {
        public Persona(string nome, string cognome, string email, string data_di_nascita, string telefono, int numero_preferito)
        {
		    if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("La persona deve avere un nome");
            }
			if (string.IsNullOrWhiteSpace(cognome))
            {
                throw new ArgumentException("La persona deve avere un cognome");
            }   

            Nome = nome;
            Cognome = cognome;
            Email = email;
            Data_di_nascita = data_di_nascita;
            Telefono = telefono;
            Numero_preferito = numero_preferito;
        }
     

        public int PersonaID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Data_di_nascita { get; set; }
        public string Telefono { get; set; }
        public int Numero_preferito { get;  set; }
        public string Email { get; set; }


    }
}
