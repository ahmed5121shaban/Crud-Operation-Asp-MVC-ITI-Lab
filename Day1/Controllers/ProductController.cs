using Maneger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using ModelView;
using System.Linq.Expressions;
using System.Security.Policy;


namespace Day1.Controllers
{

    public class ProductController : Controller
    {
        ProductManeger ProductManeger;
        MyDBContext dbContext;
        CloudinaryService cloudinaryService;

        public ProductController(ProductManeger product,MyDBContext _dbContext, CloudinaryService CloudinaryService)
        {
            this.ProductManeger = product;
            this.dbContext = _dbContext;
            this.cloudinaryService = CloudinaryService;
        }
        public IActionResult GetAll(string columnOrder = "Id", int categoryID = 0,
            int price = 0, string productName = "",
            bool IsAscending = false, int PageSize = 4, int PageNumber = 1)
        {
            Pagination<Product> products = ProductManeger.GetAllWithFilter(columnOrderBy: columnOrder,
                IsAscending: IsAscending, productName: productName,
                categoryID: categoryID, price: price, PageSize: PageSize, PageNumber: PageNumber);

            ViewData["products"] = products.Products;
            return View(products);
        }

        public IActionResult GetDetailsByID(int id)
        {
            ProductViewModel pro = ProductManeger.GetByID(id).Single().MapToView();
            ViewBag.product = pro;
            return View();
        }

        public IActionResult Delete(int id)
        {
            var product = ProductManeger.GetAll().FirstOrDefault(i => i.ID == id);
            if (product != null)
            {
                foreach (var item in product.ProductAttachments)
                {
                    if (System.IO.File.Exists($"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.Image}"
                      ))
                    {
                        System.IO.File.Delete($"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.Image}");
                    }
                }
                ProductManeger.Delete(ProductManeger.GetAll().FirstOrDefault(i => i.ID == id));

            }
            return RedirectToAction("getall", "product");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel pro)
        {
            if (!ModelState.IsValid) { 
                return View("UpdatePage", pro);
            }
            if (pro.DeleteOrNot)
            {

                var attachments = ProductManeger.GetAll().Where(p => p.ID == pro.ID).FirstOrDefault();
                foreach (var item in attachments.ProductAttachments)
                {
                    if (System.IO.File.Exists($"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.Image}"
                       ))
                    {
                        System.IO.File.Delete($"{ Directory.GetCurrentDirectory()}/wwwroot/Images/{ item.Image}");
                    }
                }
                attachments.ProductAttachments.Clear();
                dbContext.SaveChanges();

            }
            foreach (IFormFile item in pro.Attachments)
            {
                var imageUrl = await cloudinaryService.UploadFileAsync(item);
                //var data = $"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.FileName}";
                //FileStream fileStream = new FileStream(data, FileMode.Create);
                //item.CopyTo(fileStream);
                //fileStream.Close();
                pro.ProductsImageList.Add(imageUrl);
            }
            ProductManeger.Update(pro);
                ViewBag.product = pro;
                return RedirectToAction("getall", "product");
        }

        public IActionResult UpdatePage(int id)
        {
            ProductViewModel pro = ProductManeger.GetAll().Where(p=>p.ID==id).Single().MapToView();
            ViewBag.product = pro;
            return View() ;
        }

        [HttpPost]
        public  async Task<IActionResult> Add(ProductViewModel pro)
        {
            if (!ModelState.IsValid && pro.Attachments != null)
            {
                return View("addpage", pro); 
            }
            if (pro.Attachments != null)
            {
                foreach (IFormFile item in pro.Attachments)
                {
                    var imageUrl =await cloudinaryService.UploadFileAsync(item);
                    //var data = $"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.FileName}";
                    //FileStream fileStream = new FileStream(data, FileMode.Create);
                    //item.CopyTo(fileStream);
                    //fileStream.Close();
                    pro.ProductsImageList.Add(imageUrl);
                }
            }
            
            ProductManeger.Add(pro);
            return RedirectToAction("getall", "product");

            
        }

        public IActionResult AddPage() {

            return View();
        }
    }
}
