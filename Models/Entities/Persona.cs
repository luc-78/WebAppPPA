using System;
using System.Collections.Generic;
//using MyCourse.Models.ValueObjects;

namespace WebAppPPA.Models.Entities
{
    public partial class Persona
    {
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

        public int PersonaID { get; private set; }
        public string Nome { get; private set; }
        public string Cognome { get; private set; }
        public string Data_di_nascita { get; private set; }
        public int Telefono { get; private set; }
        public string Email { get; private set; }


 /*       public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("The course must have a title");
            }
            Title = newTitle;
        }

        public void ChangePrices(Money newFullPrice, Money newDiscountPrice)
        {
            if (newFullPrice == null || newDiscountPrice == null)
            {
                throw new ArgumentException("Prices can't be null");
            }
            if (newFullPrice.Currency != newDiscountPrice.Currency)
            {
                throw new ArgumentException("Currencies don't match");
            }
            if (newFullPrice.Amount < newDiscountPrice.Amount)
            {
                throw new ArgumentException("Full price can't be less than the current price");
            }
            FullPrice = newFullPrice;
            CurrentPrice = newDiscountPrice;
        }

        public virtual ICollection<Lesson> Lessons { get; private set; }*/
    }
}
