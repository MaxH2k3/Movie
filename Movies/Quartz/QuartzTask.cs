using Movies.Repository;
using Quartz;
using WatchDog;

namespace Movies.Quartz;

[DisallowConcurrentExecution]
public class QuartzTask : IJob
{
    private readonly IAnalystService _analystService;

    public QuartzTask(IAnalystService analystRepository)
    {
        _analystService = analystRepository;
    }

    public Task Execute(IJobExecutionContext context)
    {
        WatchLogger.Log("QuartzTask is running.");
        _analystService.ConvertToPrevious();
        WatchLogger.Log("QuartzTask is done.");
        
        
        return Task.CompletedTask;
    }
}