using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext1")
        {
            InitializeDbContext();
        }

        public AppDbContext(string connectionName) : base(connectionName)
        {
            InitializeDbContext();
        }
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<AdventureType> AdventureTypes { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Village> Villages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureMembershipRebootUserAccounts<UserAccount>();
        }

        private void InitializeDbContext()
        {
            Database.SetInitializer(new AppDbInitializer());
            this.RegisterUserAccountChildTablesForDelete<UserAccount>();
        }

    }
}
