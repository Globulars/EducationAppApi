using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetList();
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate);
        T GetById(int Id);
        T GetByPreducate(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> ExcuteSql(string Qry);
        void DeleteRange(IEnumerable<T> entities);
    }
}
