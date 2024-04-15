using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    ///<remarks>Darijo Bračić </remarks>
    public abstract class Repository<T> : IDisposable where T : class
    {
        protected ManageIT Context { get; set; }
        protected DbSet<T> Entities { get; set; }
        public Repository(ManageIT context)
        {
            Context = new ManageIT();
            Entities = Context.Set<T>();
        }
        ///<remarks>Darijo Bračić </remarks>
        public virtual IQueryable<T> GetAll()
        {
            var query = from x in Entities
                        select x;
            return query;
        }
        ///<remarks>Darijo Bračić </remarks>
        public virtual int Remove(T entity, bool saveChanges = true)
        {
            Entities.Attach(entity);
            Entities.Remove(entity);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }


        ///<remarks>Darijo Bračić </remarks>
        public virtual void Dispose()
        {
            Context.Dispose();
        }


        ///<remarks>Darijo Bračić </remarks>
        public virtual int Add(T entity, bool saveChanges = true) {
            Entities.Add(entity);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
        ///<remarks>Darijo Bračić </remarks>
        public virtual int Update(T entity, bool saveChanges = true)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public virtual int SaveChanges() {
            return Context.SaveChanges();

        }

    }
}

