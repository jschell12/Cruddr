using Cruddr.PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cruddr
{

    public abstract class ACruddr : ICruddr
    {
        public virtual string ERROR_MESSAGE
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Commit()
        {
            throw new NotImplementedException();
        }

        public virtual void Rollback()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Create<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Insert<TEntity>(TEntity tEntity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update<TEntity>(TEntity tEntity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Delete<TEntity>(TEntity tEntity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IPagedList<TEntity> GetPage<TEntity>(int skip, int take, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IPagedList<TEntity> GetPage<TEntity>(Expression<Func<TEntity, bool>> where, int skip, int take, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ILookup<TOuter, TInner> GetRelated<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            throw new NotImplementedException();
        }

        public virtual ILookup<TOuter, IEnumerable<TInner>> GetRelatedLeft<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            throw new NotImplementedException();
        }

        public virtual string ToQueryString()
        {
            throw new NotImplementedException();
        }

        public virtual void DisableLazyLoading(bool isDisabled)
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }


        public virtual void LoadData(Action<ICruddr> loadData)
        {
            throw new NotImplementedException();
        }
    }
}
