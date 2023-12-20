using APIProject.Models;
using APIProject.Data;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<InterestLink> InterestLinks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
