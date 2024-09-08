using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public static class ProductViewModelExetention
    {
        public static Product MapUpdate (this ProductViewModel productView)
        {
            List<ProductAttachment> imagesPathes = null;


                imagesPathes = new List<ProductAttachment>();
                foreach (IFormFile item in productView.productAttachments)
                {
                    imagesPathes.Add(new ProductAttachment { Image = item.FileName });
                }
            
            return new Product { CategoryID = productView.CategoryID,ID = (int)productView.ID,
                Price = productView.Price, Name = productView.Name, Quantity = productView.Quantity,
                 Description = productView.Description ,
                ProductAttachments = imagesPathes,
            };
        }
        public static Product MapAdd(this ProductViewModel productView)
        {
            List<ProductAttachment> imagesPathes=null;


                imagesPathes = new List<ProductAttachment>();
                foreach (IFormFile item in productView.productAttachments)
                {
                    imagesPathes.Add(new ProductAttachment { Image= item.FileName });
                }
            

            return new Product
            {
                CategoryID = productView.CategoryID,
                Price = productView.Price,
                Name = productView.Name,
                Quantity = productView.Quantity,
                Description = productView.Description,
                ProductAttachments = imagesPathes,
            };
        }
        public static ProductViewModel MapToView(this Product productView)
        {
            List<string> paths = new List<string>();
            foreach (var item in productView.ProductAttachments)
            {
                paths.Add(item.Image);
            }

            return new ProductViewModel
            {
                CategoryID = productView.CategoryID,
                ID = (int)productView.ID,
                Price = productView.Price,
                Name = productView.Name,
                Quantity = productView.Quantity,
                Description = productView.Description,
                productsImageList= paths
            };
        }
        
    }
}
