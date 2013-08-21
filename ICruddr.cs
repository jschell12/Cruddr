using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
using Cruddr.PagedList;


namespace Cruddr
{
    public interface ICruddr : IDisposable
    {
        string ERROR_MESSAGE { get; }
        int Commit();
        void Rollback();
        TEntity Create<TEntity>() where TEntity : class;
        TEntity Insert<TEntity>(TEntity tEntity) where TEntity : class;
        TEntity Update<TEntity>(TEntity tEntity) where TEntity : class;
        TEntity Delete<TEntity>(TEntity tEntity) where TEntity : class;
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;

        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class;

        IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class;

        IPagedList<TEntity> GetPage<TEntity>(int skip, int take, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties)
           where TEntity : class;
        IPagedList<TEntity> GetPage<TEntity>(Expression<Func<TEntity, bool>> where, int skip, int take, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties)
          where TEntity : class;

        ILookup<TOuter, TInner> GetRelated<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class;

        ILookup<TOuter, IEnumerable<TInner>> GetRelatedLeft<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
          Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class;

        string ToQueryString();
        void DisableLazyLoading(bool isDisabled);
    }

    public interface ICruddrImpl<TContext> : ICruddr
    {
    }
    public interface ICruddrContext : ICruddr
    {
    }
}