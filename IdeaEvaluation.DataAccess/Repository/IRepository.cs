using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace IdeaEvaluation.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Commands

        void Create(TEntity entity);

        void Update(TEntity entity);

        void delete(int id);

        Task<int> SaveChangesAsync();

        #endregion

        #region Async Queries

        Task<TEntity> GetAsync(Expression<Func<TEntity,bool>> predicate=null,
        Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
        bool disableTracking=true,
        params Expression<Func<TEntity,object>>[] includes );

        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity,bool>> predicate=null,
        Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
        bool disableTracking=true,
        params Expression<Func<TEntity,object>>[] includes );

        #endregion

    }
}