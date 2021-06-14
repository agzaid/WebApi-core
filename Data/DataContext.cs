using Microsoft.EntityFrameworkCore;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options) { }
        public DbSet<Character> characters { get;set; }
        public DbSet<User> Users{ get;set; }
    }
}
