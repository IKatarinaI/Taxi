using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TaxiService.Models;

namespace TaxiService.Persistance
{
    public class Context : DbContext
    {
        public Context() : base("TaxiServiceDB")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
    }
}