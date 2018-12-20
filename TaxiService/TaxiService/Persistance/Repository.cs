using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaxiService.Persistance.Interface;

namespace TaxiService.Persistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context db;

        public Repository(Context context)
        {
            db = context;
        }

        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public T Get(T id)
        {
            return db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public T Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return (T)db.Set<T>().Where(predicate).FirstOrDefault();
        }
    }
}
