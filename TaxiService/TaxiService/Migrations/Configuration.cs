namespace TaxiService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaxiService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaxiService.App_Start.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaxiService.App_Start.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if(!context.Users.Any(u => u.Username=="admin"))
            {
                var admin = new User() { Id = 1, Gender = GENDER.FEMALE.ToString(), JMBG = "14221995835495", Phone = "0694219940", Rides = new System.Collections.Generic.List<Ride>(), Name = "admin", LastName = "admin", Username = "admin", Password = "admin", Email = "admin@admin.admin", Role = "ADMIN" };
                context.Users.Add(admin);
            }

            if (!context.Users.Any(u => u.Username == "admin1"))
            {
                var admin1 = new User() { Id = 2, Gender = GENDER.FEMALE.ToString(), JMBG = "24321995835495", Phone = "0694219940", Rides = new System.Collections.Generic.List<Ride>(), Name = "admin1", LastName = "admin1", Username = "admin1", Password = "admin1", Email = "admin1@admin.admin", Role = "ADMIN" };
                context.Users.Add(admin1);
            }

            if (!context.Users.Any(u => u.Username == "admin2"))
            {
                var admin2 = new User() { Id = 3, Gender = GENDER.FEMALE.ToString(), JMBG = "35495995835495", Phone = "0694219940", Rides = new System.Collections.Generic.List<Ride>(), Name = "admin2", LastName = "admin2", Username = "admin2", Password = "admin2", Email = "admin2@admin.admin", Role = "ADMIN" };
                context.Users.Add(admin2);
            }
            
            context.SaveChanges();
        }
    }
}
