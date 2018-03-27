using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace FMS.DataAccess
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
      where TContext : DbContext
      where TEntity : class
    {
        protected readonly TContext Context;

        protected GenericRepository(TContext context)
        {
            this.Context = context;
        }

        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }

        public void Update(TEntity model, EntityState state)
        {
            //it works  Context.Set<TEntity>().AddOrUpdate(model); it comes in using System.Data.Entity.Migrations;
            
            Context.Set<TEntity>().Attach(model);
            Context.Entry(model).State = state;
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

    }
}
