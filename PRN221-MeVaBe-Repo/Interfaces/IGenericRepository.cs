using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null, // Optional parameter for pagination (page number)
            int? pageSize = null);


        public T GetByID(object id);

        public void Insert(T entity);

        public void Delete(object id);

        public void Delete(T entityToDelete);

        public void Update(T entityToUpdate);
        public int Count(IList<T> entities, int pageSize);
    }
}
