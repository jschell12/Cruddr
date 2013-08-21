using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Cruddr
{
    public static class QueryExtensions
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }



        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, params string[] includes)
        where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }

        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, TEntity entity, params string[] includes)
        where TEntity : class
        {
            if (includes != null)
            {
                foreach (var inc in includes)
                {
                    query = query.Include(inc);
                }
            }
            return query;
        }


        public static TEntity LoadAllChild<TEntity>(this TEntity source)
        {

            List<PropertyInfo> PropList = (from a in source.GetType().GetProperties()
                                           where a.PropertyType.Name == "Collection"
                                           select a).ToList();
            foreach (PropertyInfo prop in PropList)
            {
                object instance = prop.GetValue(source, null);
                bool isLoad =
                  (bool)instance.GetType().GetProperty("IsLoaded").GetValue(instance, null);
                if (!isLoad)
                {
                    MethodInfo mi = (from a in instance.GetType().GetMethods()
                                     where a.Name == "RunQuery" && a.GetParameters().Length == 0
                                     select a).FirstOrDefault();

                    mi.Invoke(instance, null);
                }
            }
            return (TEntity)source;
        }
    }
}