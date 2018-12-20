using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaxiService.Models;
using TaxiService.Persistance.Interface;

namespace TaxiService.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context db;

        public IRepository<Address> AddressRepository => new Repository<Address>(db);
        public IRepository<Car> CarRepository => new Repository<Car>(db);
        public IRepository<Comment> CommentRepository => new Repository<Comment>(db);
        public IRepository<Driver> DriverRepository => new Repository<Driver>(db);
        public IRepository<Location> LocationRepository => new Repository<Location>(db);
        public IRepository<Ride> RideRepository => new Repository<Ride>(db);
        public IRepository<User> UserRepository => new Repository<User>(db);

        public UnitOfWork(Context context)
        {
            db = context;
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Discard()
        {
            foreach (var entity in db.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entity.Reload();
                        break;
                }
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
