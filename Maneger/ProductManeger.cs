using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using Models;
using ModelView;
using System.Linq.Expressions;

namespace Maneger
{
    public class ProductManeger:MainManeger<Product>
    {
        public ProductManeger(MyDBContext dbContextOptions):base(dbContextOptions)
        { 
            
        }
        public IQueryable<Product> GetByID(int id)
        {
            
            return base.GetAll().Where(p => p.ID == id); ;
        }

        public IQueryable<Product> GetAllWithFilter(Expression<Func<Product, bool>> expression,
           string columnOrderBy = "Id", bool IsAscending = false, string columnSearch="",
           int PageSize = 4, int PageNumber = 1, double price = 1)
        {
            ExpressionStarter<Product> builder = PredicateBuilder.New(expression);
            var old = builder;
            if (!string.IsNullOrEmpty(columnSearch))
            {
                builder = builder.Or(p => p.Name.Contains(columnSearch));
            }
            if (price > 0)
            {
                builder = builder.Or(p => p.Price <= price);
            }

            if (old == builder)
            {
                builder = null;
            }
            var quary = base.Filter(builder, columnOrderBy, IsAscending, PageSize, PageNumber);
            return null;
        }

        public void  Update(ProductViewModel pro)
        {

            base.Update(pro.MapUpdate());

        }

        public void Add(ProductViewModel pro)
        {
               

                base.Add(pro.MapAdd());

        }


    }
}
