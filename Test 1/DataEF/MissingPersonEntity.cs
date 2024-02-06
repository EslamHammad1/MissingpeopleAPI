using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Test_1.DataEF
{

    public class MissingPersonEntity :IdentityDbContext<ApplicationUser>
    {
        public MissingPersonEntity()
        {
            
        }
        public MissingPersonEntity(DbContextOptions options) : base (options)
        {
            
        }
        public DbSet<FoundPerson> foundPersons { get; set; }
        public DbSet<LostPerson> lostPersons { get; set; }

    }
}
