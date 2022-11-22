using agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace agenda.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        //Agregar los modelos 
        public DbSet<Contacto> Contactos { get; set; }
    }
}
