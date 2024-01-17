using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAppPPA.Models.Entities;

namespace WebAppPPA.Models.Services.Infrastructure
{
    public partial class WebAppPPADbContext : DbContext
    {

        public WebAppPPADbContext(DbContextOptions<WebAppPPADbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Persone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persone"); //Superfluo se la tabella si chiama come la proprietà che espone il DbSet
                entity.HasKey(persona => persona.PersonaID); //Superfluo se la proprietà si chiama Id oppure CoursesId
                //entity.HasKey(course => new { course.Id, course.Author }); //Per chiavi primarie composite (è importante rispettare l'ordine dei campi)

                //Mapping per gli owned types
              /*  entity.OwnsOne(course => course.CurrentPrice, builder => {
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("CurrentPrice_Currency"); //Superfluo perché le nostre colonne seguono già la convenzione di nomi
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount"); //Superfluo perché le nostre colonne seguono già la convenzione di nomi
                });

                entity.OwnsOne(course => course.FullPrice, builder => {
                    builder.Property(money => money.Currency).HasConversion<string>();
                });

                //Mapping per le relazioni
                entity.HasMany(course => course.Lessons)
                      .WithOne(lesson => lesson.Course)
                      .HasForeignKey(lesson => lesson.CourseId); //Superflua se la proprietà si chiama CourseId
*/


            });

           
        }
    }
}
