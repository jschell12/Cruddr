using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Cruddr
{
    


    public interface IQuery<QR> : IDisposable
    {
        void RunQuery();
        void RunQuery(Expression<Func<QR, bool>> where);
        void RunQuery(Expression<Func<QR, bool>> where, int take = 10);        
        void RunQuery<TKey>(Expression<Func<QR, bool>> where, int take = 10, params Expression<Func<QR, TKey>>[] orderBy);
        IEnumerable<QR> QueryResults { get; set; }
        IQueryable<QR> Query { get; set; }
        string ToString();
    }

    public interface ISearchQuery<QR> : IQuery<QR>
    {       
        void Search(string searchTerm, int take = 10);
        void Search<TKey>(string searchTerm, int take = 10, params Expression<Func<QR, TKey>>[] orderBy);
    }

    public interface ISearchSpecific<QR> : IDisposable
    {
        void Search(string searchTerm, string type, int take = 10);
        void Search<TKey>(string searchTerm, string type, int take = 10, params Expression<Func<QR, TKey>>[] orderBy);
    }
}
