using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace WebApp.Models
{
    public class DatabaseContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=alexkuznetsov.database.windows.net;user=sashabelaz7;password=zse3rfvWWW;database=DB_Alex");
        }

    }
}
