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
    public class CategoryService
    {
        private readonly Prakt15LeshukovTrpoContext _db = DbService.Instance.Context;
        public ObservableCollection<Category> Categories { get; set; } = new();
        public int Commit() => _db.SaveChanges();

        public void Add(Category category)
        {
            var _category = new Category
            {
               
                Name = category.Name,
                

            };
            _db.Add<Category>(_category);
            Commit();
            Categories.Add(_category);
        }
        public void GetAll()
        {
            var categorys = _db.Categories
                .Include(c=>c.Products)
                .ToList();
            Categories.Clear();
            foreach (var category in categorys)
            {
                Categories.Add(category);
            }
        }
        public CategoryService()
        {
            GetAll();
        }

        public void Remove(Category category)
        {
            _db.Remove(category);
            if (Commit() > 0)
                if (Categories.Contains(category))
                    Categories.Remove(category);
        }
    }
}
