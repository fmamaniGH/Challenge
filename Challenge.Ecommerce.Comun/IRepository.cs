using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Comun
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetList();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> wherecondition = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                 string includeProps = null, int? page = null, int? pageSize = null);
        Task<T> GetById(object id);
        Task<int> Count(Expression<Func<T, bool>> filter = null);

        void DeleteEntity(T entity);
        Task Delete(object id);
        void Update(T entity);
        Task Insert(T entity);
        void DeAttach(T entity);


    }
}
