using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiService.Models;

namespace TaxiService.Persistance.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Address> AddressRepository { get; }
        IRepository<Car> CarRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Driver> DriverRepository { get; }
        IRepository<Location> LocationRepository { get; }
        IRepository<Ride> RideRepository { get; }
        IRepository<User> UserRepository { get; }

        // commit all changes
        void Commit();

        // discard all changes
        void Discard();

        void Dispose();
    }
}

