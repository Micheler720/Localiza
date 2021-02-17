using Microsoft.EntityFrameworkCore;
using Domain.Entities.Interfaces;
using Domain.Entities;

namespace Infra.Database.Implementations.EntityFramework
{
    public class ContextEntity : DbContext
    {

        public ContextEntity() { }
        public ContextEntity(DbContextOptions options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer( "Server=tcp:localizabackend.database.windows.net,1433;Initial Catalog=databaseLocaliza;Persist Security Info=False;User ID=Administrador;Password=Administr@dor;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.IdBrand);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.IdCategory);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Fuel)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.IdFuel);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.IdModel);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Car)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.IdCar);

             modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.IdClient);

             modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Operator)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.IdOperator);

            modelBuilder.Entity<CheckList>()
            .HasOne(b => b.Appointment)
            .WithOne(i => i.CheckList)
            .HasForeignKey<Appointment>(b => b.IdCheckList);
           
        }
        
        public DbSet<Appointment> Appointments { get; set;}
        public DbSet<CheckList> CheckLists { get; set;}
        public DbSet<Car> Cars { get; set;}        
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Client> Clients { get; set; }           
        public DbSet<CarBrand> car_brands { get; set; }
        public DbSet<CarCategory> car_categories { get; set;}
        public DbSet<CarFuel> car_fuels { get; set;}
        public DbSet<CarModel> car_models { get; set;}

    }
}