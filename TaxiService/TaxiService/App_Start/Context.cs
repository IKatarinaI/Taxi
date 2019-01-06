using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiService.Models;
using System.Data.Entity;

namespace TaxiService.App_Start
{
    public class Context : DbContext
    {
        public Context() : base("TaxiService")
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