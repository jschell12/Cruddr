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
    public class Cruddr : ACruddr, ICruddr
    {
        protected bool disposed = false;

        #region Cruddr
        protected ICruddrContext CTX { get; set; }
        #endregion

        public override string ERROR_MESSAGE { get { return this.CTX.ERROR_MESSAGE; } }

        /// <summary>
        /// Constructs unit of work.
        /// </summary>
        /// <param name="context"></param>
        [Inject]
        public Cruddr(ICruddrContext context)
        {
            this.CTX = context;
        }

        public Cruddr()
        {
            // Use dependency injection to create context
            //TODO: remove below

            //this.CTX = new EFCruddrContext(new ());
        }





        /// <summary>
        /// Saves changes made to the object context.
        /// </summary>
        public override int Commit()
        {
            try
            {
                return this.CTX.Commit();
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        /// <summary>
        /// Rejects and discards all changes made to the object context.
        /// </summary>
        public override void Rollback()
        {
            this.CTX.Rollback();
        }


        public override void DisableLazyLoading(bool isDisabled)
        {
            this.CTX.DisableLazyLoading(isDisabled);
        }

        public override TEntity Create<TEntity>()
        {
            return this.CTX.Create<TEntity>();
        }

        public override TEntity Insert<TEntity>(TEntity tEntity)
        {
            return this.CTX.Insert<TEntity>(tEntity);
        }

        public override TEntity Update<TEntity>(TEntity tEntity)
        {
            return this.CTX.Update<TEntity>(tEntity);
        }

        public override TEntity Delete<TEntity>(TEntity tEntity)
        {
            return this.CTX.Delete<TEntity>(tEntity);
        }

        public override bool Exists<TEntity>(Expression<Func<TEntity, bool>> where)
        {
            return this.CTX.Exists<TEntity>(where);
        }



        public override IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.CTX.GetMany<TEntity>(where, includeProperties);
        }

        public override TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.CTX.Get<TEntity>(where, includeProperties);
        }

        public override IEnumerable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.CTX.GetAll<TEntity>(includeProperties);
        }


        public override IPagedList<TEntity> GetPage<TEntity>(int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.CTX.GetPage<TEntity>(pageNumber, pageSize, orderBy, out totalRows, includeProperties);
        }

        public override IPagedList<TEntity> GetPage<TEntity>(Expression<Func<TEntity, bool>> where, int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.CTX.GetPage<TEntity>(where, pageNumber, pageSize, orderBy, out totalRows, includeProperties);
        }




        public override string ToQueryString()
        {
            return this.CTX.ToQueryString();
        }

        /// <summary>
        /// Disposes the unit of work and its context.
        /// </summary>
        public override void Dispose()
        {
            this.CTX.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}