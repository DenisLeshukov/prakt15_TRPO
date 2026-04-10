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
    public class BrandService
    {
        private readonly Prakt15LeshukovTrpoContext _db = DbService.Instance.Context;
        public ObservableCollection<Brand> Brands { get; set; } = new();
        public int Commit() => _db.SaveChanges();

        public void Add(Brand brand)
        {
            var _brand = new Brand
            {
                
                Name = brand.Name,


            };
            _db.Add<Brand>(_brand);
            Commit();
            Brands.Add(_brand);
        }
        public void GetAll()
        {
            var brands = _db.Brands
                .Include(c=>c.Products)
                .ToList();

            Brands.Clear();
            foreach (var brand in brands)
            {
                Brands.Add(brand);
            }
        }
        public BrandService()
        {
            GetAll();
        }

        public void Remove(Brand brand)
        {
            _db.Remove(brand);
            if (Commit() > 0)
                if (Brands.Contains(brand))
                    Brands.Remove(brand);
        }
    }
}
