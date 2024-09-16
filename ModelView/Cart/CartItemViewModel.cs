using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView { 
    public class CartItemViewModel
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double SupPrice { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string UserID { get; set; }

    }
}
