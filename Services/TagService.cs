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
    public class TagService
    {
        private readonly Prakt15LeshukovTrpoContext _db = DbService.Instance.Context;
        public ObservableCollection<Tag> Tags { get; set; } = new();
        public int Commit() => _db.SaveChanges();

        public void Add(Tag tag)
        {
            var _tag = new Tag
            {

                Name = tag.Name,


            };
            _db.Add<Tag>(_tag);
            Commit();
            Tags.Add(_tag);
        }
        public void GetAll()
        {
            var tags = _db.Tags
                .Include(t=>t.Products)
                .ToList();
            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
        }
        public TagService()
        {
            GetAll();
        }

        public void Remove(Tag tag)
        {
            _db.Remove(tag);
            if (Commit() > 0)
                if (Tags.Contains(tag))
                    Tags.Remove(tag);
        }
    }
}

