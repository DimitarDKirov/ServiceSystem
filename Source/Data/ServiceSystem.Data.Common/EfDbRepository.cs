using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Common.Models;
using ServiceSystem.Infrastructure.DateProvider;

namespace ServiceSystem.Data.Common
{
    // TODO: Why BaseModel<int> instead BaseModel<TKey>?
    // Add implemmetation with generic Id type
    public class EfDbRepository<T> : IEfDbRepository<T>
        where T : BaseModel<int>
    {
        private readonly DbContext context;
        private readonly IDbSet<T> dbSet;

        public EfDbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.dbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.dbSet;
        }

        public T GetById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTimeProvider.Current.UtcNow;
        }

        // public void HardDelete(T entity)
        // {
        //    this.DbSet.Remove(entity);
        // }
        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void HardDelete(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
