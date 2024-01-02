using Movies.Models;

namespace Movies.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
