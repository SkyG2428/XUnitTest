using DAL;
using Implementation.IRepository;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }
        public IEnumerable<Category> GetCategories()
        {
            var model = _context.Categories.ToList();

            return model;
        }
    }
}
