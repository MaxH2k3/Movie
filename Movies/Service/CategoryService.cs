﻿using Movies.Interface;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Repository
{
    public class CategoryService : ICategoryService
    {
        private readonly MOVIESContext _context;

        public CategoryService(MOVIESContext context)
        {
            _context = context;
        }

        public CategoryService()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
