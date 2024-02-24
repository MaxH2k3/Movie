using Movies.Models;
using Movies.Repository;

namespace Movies.Service;

public class NationService : INationService
{
    private readonly MOVIESContext _context;

    public NationService(MOVIESContext context)
    {
        _context = context;
    }

    public NationService()
    {
        _context = new MOVIESContext();
    }

    public IEnumerable<Nation> GetNations()
    {
        return _context.Nations.ToList();
    }

}
