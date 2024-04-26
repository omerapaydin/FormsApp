using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
{
    public static class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
           _categories.Add(new Category{CategoryId=1,Name="Telefon"}); 
           _categories.Add(new Category{CategoryId=2,Name="Bilgisayar"}); 

           _products.Add(new Product{ProductId=1,Name="Iphone 14",Price=40000,IsActive=true,Image="1.jpeg",CategoryId=1});
           _products.Add(new Product{ProductId=1,Name="Iphone 15",Price=50000,IsActive=true,Image="2.jpeg",CategoryId=1});
           _products.Add(new Product{ProductId=1,Name="Iphone 16",Price=60000,IsActive=true,Image="3.jpeg",CategoryId=1});
           _products.Add(new Product{ProductId=1,Name="Iphone 17",Price=70000,IsActive=true,Image="4.jpeg",CategoryId=1});
           _products.Add(new Product{ProductId=1,Name="Macbook Air",Price=80000,IsActive=true,Image="5.jpeg",CategoryId=2});
           _products.Add(new Product{ProductId=1,Name="Macbook Pro",Price=90000,IsActive=true,Image="6.jpeg",CategoryId=2});


        }

        public static List<Product> Products {
            get {
                return _products;
            }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }

        public static void EditProduct(Product updateProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);

            if(entity != null)
            {
                entity.Name = updateProduct.Name;
                entity.Price = updateProduct.Price;
                entity.Image = updateProduct.Image;
                entity.CategoryId = updateProduct.CategoryId;
                entity.IsActive = updateProduct.IsActive;
            }


        }

        public static void DeleteProduct(Product deleteProduct)
        {
             var entity = _products.FirstOrDefault(p => p.ProductId == deleteProduct.ProductId);
            _products.Remove(entity);
        }


        public static List<Category> Categories {
            get {
                return _categories;
            }
        }

    }
}