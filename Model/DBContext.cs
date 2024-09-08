using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductAttachment> ProductAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttachmentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
