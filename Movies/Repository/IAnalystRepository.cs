namespace Movies.Repository;

public interface IAnalystRepository
{
    Task AddViewerMovie(Guid movieId);
    Task ConvertToPrevious();
}
