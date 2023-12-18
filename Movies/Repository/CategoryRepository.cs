using Microsoft.EntityFrameworkCore;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MOVIESContext _context;

        public CategoryRepository(MOVIESContext context)
        {
            _context = context;
        }

        public CategoryRepository()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Category> GetCategories()
        {
            Debug.WriteLine("GetCategories");
            return _context.Categories.ToList();
        }
    }
}
