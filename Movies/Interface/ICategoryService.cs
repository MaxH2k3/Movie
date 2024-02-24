using Movies.Models;

namespace Movies.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
