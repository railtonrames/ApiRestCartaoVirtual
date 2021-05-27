using ApiRestCartaoVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Data
{
    public class EmailContext : DbContext
    {
        public DbSet<Email> Email { get; set; }
        public DbSet<Cartao> Cartao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=sa123456;Persist Security Info=True;User ID=sa;Initial Catalog=CartaoVirtualApp;Data Source=DESKTOP-UQDBKFD");
        }
    }
}
