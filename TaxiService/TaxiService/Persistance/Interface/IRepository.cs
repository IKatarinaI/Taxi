using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiService.Persistance.Interface
{
    public interface IRepository<T> where T : class
    {
        // create object and add to db
        void Add(T entity);

        // read object from db with provided id
        T Get(T id);

        // read all objects from db
        IEnumerable<T> GetAll();

        // read object from db with provided id
        T GetById(int id);

        // update entity from db
        void Update(T entity);

        // deletes entity from db
        void Remove(T entity);

        // returns all objects from db which correspond to given predicate
        T Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}

