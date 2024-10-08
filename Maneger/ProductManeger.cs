﻿using LinqKit;
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

        public Pagination<Product> GetAllWithFilter(
           string columnOrderBy = "Id", bool IsAscending = false, string productName = "", int categoryID=0,
           int PageSize = 4, int PageNumber = 1, double price = 1)
        {
            ExpressionStarter<Product> builder = PredicateBuilder.New<Product>();
            var old = builder;
            if (!string.IsNullOrEmpty(productName))
            {
                builder = builder.Or(p => p.Name.Contains(productName));
            }
            if (price > 0)
            {
                builder = builder.Or(p => p.Price <= price);
            }
            if (categoryID > 0)
            {
                builder = builder.Or(p => p.CategoryID <= categoryID);
            }

            if (old == builder)
            {
                builder = null;
            }

            var quary = base.Filter(builder, columnOrderBy, categoryID,price, productName, IsAscending, PageSize, PageNumber);
            return new Pagination<Product> { PageCount = PageSize , PageNum = PageNumber ,
                Total = base.GetAll().Count(),
                Products = quary.ToList()

            };
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
