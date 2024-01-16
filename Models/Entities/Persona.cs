using System;
using System.Collections.Generic;
//using MyCourse.Models.ValueObjects;

namespace WebAppPPA.Models.Entities
{
    public partial class Persona
    {
        public Persona(string nome, string cognome, string email, string datadinascita, string tel)
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
            Data_di_nascita=datadinascita;
            Telefono = tel;
        }
        public Persona(string nome, string cognome, string email)
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
        }

        public int PersonaID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Data_di_nascita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }


    }
}
