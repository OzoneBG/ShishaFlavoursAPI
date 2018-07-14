namespace ShishaFlavoursAPI.Data.Common.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        Task<T> GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task Delete(int id);

        void Detach(T entity);

        Task<int> SaveChanges();
    }
}
