using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Xml.Linq;
using System.Linq.Expressions;
using Ninject;
using Cruddr.PagedList;

namespace Cruddr
{
    /// <summary>
    /// This class wraps the functionality of any ICruddr implementation to be
    /// exposed to any calling class from an external library referencing Suppliers.Data. 
    /// It implementsthe ICruddr interface and calls the ICruddr concrete 
    /// implementation's methods.
    /// </summary>
    public class Cruddr : ISuppliersContext
    {
        protected bool disposed = false;

        #region Cruddr
        protected ICruddr UOW { get; set; }
        #endregion



        public virtual string ERROR_MESSAGE { get { return this.UOW.ERROR_MESSAGE; } }



        /// <summary>
        /// Constructs unit of work.
        /// </summary>
        /// <param name="context"></param>
        [Inject]
        public Cruddr(ICruddr context)
        {
            this.UOW = context;
        }

        public Cruddr()
        {
            // Use dependency injection to create context
            //TODO: remove below

            this.UOW = new EFCruddr(new ExampleModelContainer());
        }





        /// <summary>
        /// Saves changes made to the object context.
        /// </summary>
        public virtual int Commit()
        {
            try
            {
                return this.UOW.Commit();
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        /// <summary>
        /// Rejects and discards all changes made to the object context.
        /// </summary>
        public virtual void Rollback()
        {
            this.UOW.Rollback();
        }


        public void DisableLazyLoading(bool isDisabled)
        {
            this.UOW.DisableLazyLoading(isDisabled);

        }



        public virtual TEntity Create<TEntity>() where TEntity : class
        {
            return this.UOW.Create<TEntity>();
        }


        public virtual TEntity Insert<TEntity>(TEntity tEntity) where TEntity : class
        {
            return this.UOW.Insert<TEntity>(tEntity);
        }

        public virtual TEntity Update<TEntity>(TEntity tEntity) where TEntity : class
        {
            return this.UOW.Update<TEntity>(tEntity);
        }

        public virtual TEntity Delete<TEntity>(TEntity tEntity) where TEntity : class
        {
            return this.UOW.Delete<TEntity>(tEntity);
        }

        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : class
        {
            return this.UOW.Exists<TEntity>(where);
        }



        public virtual IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return this.UOW.GetMany<TEntity>(where, includeProperties);
        }

        public virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return this.UOW.Get<TEntity>(where, includeProperties);
        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return this.UOW.GetAll<TEntity>(includeProperties);
        }


        public virtual IPagedList<TEntity> GetPage<TEntity>(int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return this.UOW.GetPage<TEntity>(pageNumber, pageSize, orderBy, out totalRows, includeProperties);
        }

        public virtual IPagedList<TEntity> GetPage<TEntity>(Expression<Func<TEntity, bool>> where, int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows,  params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return this.UOW.GetPage<TEntity>(where, pageNumber, pageSize, orderBy, out totalRows, includeProperties);
        }

        /// <summary>
        /// Inner Join
        /// </summary>
        /// <typeparam name="TOuter"></typeparam>
        /// <typeparam name="TInner"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="outerKeySelector"></param>
        /// <param name="innerKeySelector"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public ILookup<TOuter, TInner> GetRelated<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            return this.UOW.GetRelated<TOuter, TInner, TKey>(outerKeySelector, innerKeySelector, where);
        }


        /// <summary>
        /// Left Join
        /// </summary>
        /// <typeparam name="TOuter"></typeparam>
        /// <typeparam name="TInner"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="outerKeySelector"></param>
        /// <param name="innerKeySelector"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public ILookup<TOuter, IEnumerable<TInner>> GetRelatedLeft<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
           Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            return this.UOW.GetRelatedLeft<TOuter, TInner, TKey>(outerKeySelector, innerKeySelector, where);
        }



        public string ToQueryString()
        {
            return this.UOW.ToQueryString();
        }

        /// <summary>
        /// Disposes the unit of work and its context.
        /// </summary>
        public void Dispose()
        {
            this.UOW.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

