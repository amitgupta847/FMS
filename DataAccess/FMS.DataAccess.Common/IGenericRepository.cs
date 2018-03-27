using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.DataAccess
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        void Add(T model);

        void Update(T model, EntityState state);

        void Remove(T model);
    }
}
