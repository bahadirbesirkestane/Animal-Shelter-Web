using Animal_Shelter_WebProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Animal_Shelter_WebProject.Data
{
    // Veritabanı tabloları
    public class Animal_Shelter_WebProjectDBContext : DbContext
    {
        public Animal_Shelter_WebProjectDBContext(DbContextOptions<Animal_Shelter_WebProjectDBContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
