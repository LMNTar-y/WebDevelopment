using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevelopment.Domain
{
    public interface IDefaultRepository<T, V>
    {
        Task<IEnumerable<T>> GetAll();
        Task<V> Add(V item);
        Task<T> Update(T itemWithId);
    }
}
