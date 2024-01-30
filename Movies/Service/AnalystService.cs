using Movies.Models;
using Movies.Repository;

namespace Movies.Service
{
    public class AnalystService : IAnalystRepository
    {
        private readonly MovieMongoContext _context;

        public AnalystService(MovieMongoContext context)
        {
            _context = context;
        }

        public AnalystService()
        {
            _context = new MovieMongoContext();
        }

        public void Add(Guid movieId)
        {
            
        }

    }
}
