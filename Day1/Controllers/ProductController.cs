using Maneger;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelView;


namespace Day1.Controllers
{
    public class ProductController : Controller
    {
        ProductManeger ProductManeger;
        MyDBContext dbContext;
        public ProductController(ProductManeger product,MyDBContext _dbContext)
        {
            this.ProductManeger = product;
            this.dbContext = _dbContext;
        }
        public IActionResult GetAll()
        {
            List<Product> products = ProductManeger.GetAll().ToList();
            ViewData["products"] = products;
            return View();
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
        public IActionResult Update(ProductViewModel pro)
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
            foreach (IFormFile item in pro.productAttachments)
            {
                var data = $"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.FileName}";
                FileStream fileStream = new FileStream(data, FileMode.Create);
                item.CopyTo(fileStream);
                fileStream.Close();
                pro.productsImageList.Add(item.FileName);
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
        public IActionResult Add(ProductViewModel pro)
        {
            if (!ModelState.IsValid)
            {
                return View("addpage", pro); 
            }

                foreach (IFormFile item in pro.productAttachments)
                {
                    var data = $"{Directory.GetCurrentDirectory()}/wwwroot/Images/{item.FileName}";
                    FileStream fileStream = new FileStream(data, FileMode.Create);
                    item.CopyTo(fileStream);
                    fileStream.Close();
                    pro.productsImageList.Add(item.FileName);
                }
                ProductManeger.Add(pro);
                return RedirectToAction("getall", "product");

            
        }

        public IActionResult AddPage() {

            return View();
        }
    }
}
