using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdeaEvaluation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace IdeaEvaluation.DataAccess.Repository
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {
       protected DbSet<TEntity> appDbSet;
       protected IdeaEvaluationDBContext _appdbContext;
       public Repository(IdeaEvaluationDBContext appdbContext)
       {
           _appdbContext=appdbContext;
          appDbSet=_appdbContext.Set<TEntity>();
       }

        public virtual void Create(TEntity entity)
        {
            if(entity==null)
            throw new ArgumentNullException(nameof(entity));
            appDbSet.Add(entity);
        }

       public virtual void Update(TEntity entity)
        {
             if(entity==null)
            throw new ArgumentNullException(nameof(entity));
            appDbSet.Update(entity);
        }
        public virtual void delete(int id)
        {
           var entity=appDbSet.Find(id);
           if(entity!=null)
              appDbSet.Remove(entity);
        }


        public async Task<int> SaveChangesAsync()
        {
           return await _appdbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query =appDbSet;
            if(disableTracking)
            query=query.AsNoTracking();

            if(includes.Any())
             query= includes.Aggregate(query,(current,include)=>current.Include(include));
       
             if(predicate!=null)
             query=query.Where(predicate);
              
              if(orderBy!=null)
             return await orderBy(query).FirstOrDefaultAsync();

           return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query =appDbSet;
            if(disableTracking)
            query=query.AsNoTracking();

            if(includes.Any())
             query= includes.Aggregate(query,(current,include)=>current.Include(include));
       
             if(predicate!=null)
             query=query.Where(predicate);
              
              if(orderBy!=null)
             return await orderBy(query).ToListAsync();
              else
              return await query.ToListAsync();

        }



      
    }
}