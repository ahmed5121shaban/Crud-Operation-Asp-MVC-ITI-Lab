
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ModelView
{
    public class ProductViewModel
    {

        public int? ID { get; set; }
        [Required(ErrorMessage = "Product Name Is Requird")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You Must Write The Discription")]
        [MultiLine]
        public string Description { get; set; }
        [Required(ErrorMessage = "You Must Enter The Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This Faild Is Required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This Faild Is Required")]
        public int CategoryID { get; set; }

        public bool DeleteOrNot {  get; set; } = false;
        public IFormFileCollection productAttachments { get; set; }
        public List<string> productsImageList { get; set; } = new List<string>();


    }

}

