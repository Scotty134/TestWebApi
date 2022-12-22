using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=DESKTOP-0D43FCJ\\SQLEXPRESS;Database=MyTestDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        //public DataContext(DbContextOptions options) : base(options)
        //{
        //}
    }
}
