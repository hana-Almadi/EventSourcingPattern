
using System.Collections.Generic;
using System.Threading.Tasks;

namespace account_event_sourcing.Repository
{
   public interface Repository<T,Id>
    {
        public Task Save(T t);
        public Task<List<T>> FindAll();
        public Task<T> FindById(Id id);
        public Task Update(T t);

    }
}
