using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infra.Context
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext()
        {
        }

        public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsnuilder)
        {
            optionsnuilder.UseSqlServer(@"Data Source=DESKTOP-9J6501O\SQLEXPRESS;Initial Catalog=USER_MANAGER;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
