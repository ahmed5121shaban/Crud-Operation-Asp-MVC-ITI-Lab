using Manager;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maneger
{
    public class MainManeger<T> where T : class
    {
        MyDBContext myDBContext;
        DbSet<T> Set;
        public MainManeger(MyDBContext context)
        {
            myDBContext = context;
            Set = myDBContext.Set<T>();
        }
        public IQueryable<T> GetAll() {
            try { 
                return Set.AsQueryable();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.InnerException);
                return null; 
            }
            
        }

        public bool Delete(T entity) {
            try
            {
                myDBContext.Remove(entity);
                myDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.InnerException); 
                return false;

            }
        }

        public bool Update(T entity) {
           try {
                myDBContext.SaveChanges();
                myDBContext.Update(entity);
                myDBContext.SaveChanges();
                return true;
            } catch (Exception ex) { 
                Console.WriteLine(ex.InnerException);
                return false; 
            }
        }

        public bool Add(T e)
        {
            try
            {
                myDBContext.Add(e);
                myDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.InnerException);
                return false;
            }
            
        }


        public IQueryable<T> Filter(Expression<Func<T, bool>> expression, string columnOrder = "Id", int categoryID = 0,
            double price = 0,string productName = "",
            bool IsAscending = false,int PageSize = 4, int PageNumber = 1)
        {
            IQueryable<T> query = myDBContext.Set<T>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);


            if (!string.IsNullOrEmpty(columnOrder))
                query = query.OrderBy(columnOrder, IsAscending);


            if (PageNumber < 0)
            {
                PageNumber = 1;
            }
            if (PageSize < 0)
            {
                PageSize = 5;
            }
    
            if (query.Count() < PageSize)
            {
                PageSize = query.Count();
                PageNumber = 1;
            }

            int Skip = (PageNumber - 1) * PageSize;
            query = query.Skip(Skip).Take(PageSize);

            return query;
        }


    

}
}
