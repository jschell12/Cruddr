using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using System.Xml.Linq;
using System.Linq.Expressions;
using Ninject;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Cruddr.PagedList;

namespace Cruddr
{
    /// <summary>
    /// This is a Cruddr-EntityFramework wrapper.
    /// </summary>
    public class EFCruddrContext : ICruddrContext<DbContext>
    {
        protected bool disposed = false;

        #region Context
        protected DbContext context;


        protected DbContext Context { get; set; }
        #endregion




        #region queryString
        private string queryString;
        #endregion






        /// <summary>
        /// Constructs unit of work.
        /// </summary>
        /// <param name="context"></param>
        [Inject]
        public EFCruddrContext(ExampleModelContainer context)
        {
            this.context = context;

        }

        public EFCruddrContext()
        {
            // Use dependency injection to create context
            this.context = new ExampleModelContainer();
        }

        protected string errorMessage;
        public virtual string ERROR_MESSAGE { get { return this.errorMessage; } }


        /// <summary>
        /// Saves changes made to the object context.
        /// </summary>
        public virtual int Commit()
        {
            try
            {
                return this.context.SaveChanges();
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return -1;
            }
        }

        /// <summary>
        /// Rejects and discards all changes made to the object context.
        /// </summary>        
        public virtual void Rollback()
        {
            //this.Dispose();            
        }


        public void DisableLazyLoading(bool isDisabled)
        {
            this.context.Configuration.LazyLoadingEnabled = !isDisabled;

        }



        public virtual TEntity Create<TEntity>()
            where TEntity : class
        {
            try
            {
                return this.context.Set<TEntity>().Create<TEntity>();
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return null;
            }
        }


        public virtual TEntity Insert<TEntity>(TEntity tEntity)
            where TEntity : class
        {
            try
            {
                this.ModifyState(tEntity);
                return this.context.Set<TEntity>().Add(tEntity);
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return tEntity;
            }
        }

        public virtual TEntity Update<TEntity>(TEntity tEntity)
            where TEntity : class
        {
            try
            {

                this.ModifyState(tEntity);
                return tEntity;
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return tEntity;
            }
        }

        public virtual TEntity Delete<TEntity>(TEntity tEntity)
            where TEntity : class
        {
            try
            {
                return this.context.Set<TEntity>().Remove(tEntity);
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return tEntity;
            }
        }



        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> where)
            where TEntity : class
        {
            try
            {
                IDbSet<TEntity> set = this.context.Set<TEntity>();

                var query = set.AsQueryable<TEntity>();

                return query.Any(where);
            }
            catch (Exception e)
            {
                this.errorMessage = e.ToString();
                return false;
            }
        }


        /// <summary>
        /// Gets entity based on Expression passed. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns>First or Default TEntity found in database</returns>
        public virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class
        {
            IDbSet<TEntity> set = this.context.Set<TEntity>();

            var query = set.IncludeMultiple(includeProperties).AsQueryable<TEntity>();

            query = query.Where(where);
            this.queryString = "";
            this.queryString = query.ToString();
            var entity = query.FirstOrDefault<TEntity>();




            return entity;
        }





        public virtual IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class
        {
            IDbSet<TEntity> set = this.context.Set<TEntity>();

            var query = set.IncludeMultiple(includeProperties).AsQueryable<TEntity>();

            query = query.Where(where);
            this.queryString = "";
            this.queryString = query.ToString();
            List<TEntity> list = query.ToList();
            return list;
        }




        public virtual IEnumerable<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class
        {
            IDbSet<TEntity> set = this.context.Set<TEntity>();

            var query = set.IncludeMultiple(includeProperties).AsQueryable<TEntity>();

            this.queryString = "";
            this.queryString = query.ToString();
            List<TEntity> list = query.ToList();
            return list;
        }




        public IPagedList<TEntity> GetPage<TEntity>(int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IDbSet<TEntity> set = this.context.Set<TEntity>();

            var query = set.IncludeMultiple(includeProperties).AsQueryable<TEntity>();

            this.queryString = "";
            this.queryString = query.ToString();
            query = query.OrderBy(orderBy);
            totalRows = query.Count();
            IPagedList<TEntity> pagedList = query.OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
            return pagedList;
        }

        public IPagedList<TEntity> GetPage<TEntity>(Expression<Func<TEntity, bool>> where, int pageNumber, int pageSize, Expression<Func<TEntity, object>> orderBy, out int totalRows, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IDbSet<TEntity> set = this.context.Set<TEntity>();

            var query = set.IncludeMultiple(includeProperties).AsQueryable<TEntity>();

            this.queryString = "";
            this.queryString = query.ToString();
            query = query.OrderBy(orderBy);
            totalRows = query.Count();
            IPagedList<TEntity> pagedList = query.OrderBy(orderBy).ToPagedList(pageNumber, pageSize);
            return pagedList;
        }


        public virtual ILookup<TOuter, TInner> GetRelated<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            IDbSet<TOuter> set = this.context.Set<TOuter>();
            IDbSet<TInner> setToRelate = this.context.Set<TInner>();

            var query1 = set.AsQueryable<TOuter>();
            if (where != null)
            {
                query1 = query1.Where(where);
            }
            var query2 = setToRelate.AsQueryable<TInner>();
            var queryResult = query1.Join(
                                  query2,
                                  outerKeySelector,
                                  innerKeySelector,
                                  (q1, q2) =>
                                     new
                                     {
                                         q1 = q1,
                                         q2 = q2
                                     }
                               );


            this.queryString = "";
            this.queryString = queryResult.ToString();


            var result = queryResult.ToLookup(x => x.q1, y => y.q2);



            return result;
        }


        public virtual ILookup<TOuter, IEnumerable<TInner>> GetRelatedLeft<TOuter, TInner, TKey>(Func<TOuter, TKey> outerKeySelector,
           Func<TInner, TKey> innerKeySelector,
           Expression<Func<TOuter, bool>> where = null)
            where TOuter : class
            where TInner : class
        {
            IDbSet<TOuter> set = this.context.Set<TOuter>();
            IDbSet<TInner> setToRelate = this.context.Set<TInner>();

            var query1 = set.AsQueryable<TOuter>();
            if (where != null)
            {
                query1 = query1.Where(where);
            }
            var query2 = setToRelate.AsQueryable<TInner>();
            var queryResult = query1.GroupJoin(
                                  query2,
                                  outerKeySelector,
                                  innerKeySelector,
                                  (q1, q2) =>
                                     new
                                     {
                                         q1 = q1,
                                         q2 = q2
                                     }
                               );


            this.queryString = "";
            this.queryString = queryResult.ToString();


            var result = queryResult.ToLookup(x => x.q1, y => y.q2);



            return result;
        }



        protected void ModifyState<TEntity>(TEntity tEntity) where TEntity : class
        {
            this.context.Entry(tEntity).State = EntityState.Modified;
        }


        public string ToQueryString()
        {
            return this.queryString;
        }



        /// <summary>
        /// Disposes the unit of work and its context.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    if (this.context != null)
                    {
                        // Dispose managed resources.
                        this.context.Dispose();
                    }
                }
            }

            disposed = true;
        }


       
    }
}
