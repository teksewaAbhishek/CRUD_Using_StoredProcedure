using AspNetCoreApi6.Controllers;
using AspNetCoreApi6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi6.Contextes
{

    public class AuthDemoDbContext : IdentityDbContext
    {
        public AuthDemoDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
