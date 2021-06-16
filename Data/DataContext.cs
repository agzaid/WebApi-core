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
        public DbSet<Weapon> Weapons{ get;set; }
        public DbSet<Skill> Skills{ get;set; }
        public DbSet<CharacterSkill> characterSkills{ get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterSkill>().HasKey(cs => new { cs.CharacterId, cs.SkillId });
        }
    }
}
