using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Challenge.Ecommerce.Infrastructure.Data;
using Challenge.Ecommerce.Comun;

namespace Challenge.Ecommerce.Infrastructure.Repository
{ 
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected internal readonly DbSet<T> dbSet;

        public Repository(ApplicationContext context)
        {

            _context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> wherecondition = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                string includeProps = null, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (wherecondition != null)
            {
                query = query.Where(wherecondition);
            }
            if (includeProps != null && includeProps != string.Empty)
            {
                foreach (var includProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);
                }
            }


            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            if (orderby != null)
            {
                return await orderby(query).AsNoTracking<T>().ToListAsync();
            }
            else
            {
                return await query.AsNoTracking<T>().ToListAsync();
            }
        }

        public async Task<T> GetById(object id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetList()
        {
            return await _context.Set<T>().AsNoTracking<T>().ToListAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }

        public async Task Delete(object id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
            }

        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Attach(entity).State = EntityState.Modified;

        }
        public void DeAttach(T entity)
        {
            _context.Attach(entity).State = EntityState.Detached;
        }
    }
}
