using Movies.Business.anothers;

namespace Movies.Repository;

public interface IQuartzRepository
{
    Task<JobDetails> GetCurrentJob();
    Task<string> ControlTask(int? time, string action);
    Task<string> ExecuteJob();
}