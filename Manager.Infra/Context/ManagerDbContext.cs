using Manager.Domain.Entities;
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

        public virtual DbSet<User> Users { get; set; }
    }
}
