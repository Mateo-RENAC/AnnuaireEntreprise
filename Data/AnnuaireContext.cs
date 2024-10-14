using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using AnnuaireEntreprise.Models;

namespace AnnuaireEntreprise.Data
{
    public class AnnuaireContext : DbContext
    {
        public DbSet<Models.Site> Sites { get; set; }
        public DbSet<Models.Service> Services { get; set; }
        public DbSet<Models.Employe> Employes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
