namespace ShishaFlavoursAPI.Common.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            this.Context = context ?? throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            this.DbSet = this.Context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual async Task Add(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await this.DbSet.AddAsync(entity);
            }
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual async Task Delete(int id)
        {
            var entity = await this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await this.DbSet.FindAsync(Context, id);
        }

        public virtual int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
