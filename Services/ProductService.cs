using Microsoft.EntityFrameworkCore;
using prakt15_Leshukov_TRPO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15_Leshukov_TRPO.Services
{
    public class ProductService
    {
        private readonly Prakt15LeshukovTrpoContext _db = DbService.Instance.Context;
        public ObservableCollection<Product> Products { get; set; } = new();
        public int Commit() => _db.SaveChanges();

        public void Add(Product product)
        {
            var _product = new Product
            {
               
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Raiting = product.Raiting,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Tags = product.Tags,
                Brand  = product.Brand,
                Category = product.Category,

            };
            _db.Add<Product>(_product);
            Commit();
            Products.Add(_product);
        }
        public void GetAll()
        {
            var products = _db.Products
                              .Include(p => p.Category)
                              .Include(p => p.Brand)
                              .Include(p => p.Tags)
                              .ToList();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
        public ProductService()
        {
            GetAll();
        }

        public void Remove(Product product)
        {
            _db.Remove(product);
            if (Commit() > 0)
                if (Products.Contains(product))
                    Products.Remove(product);
        }
    }
}
