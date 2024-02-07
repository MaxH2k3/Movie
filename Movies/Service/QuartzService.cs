using Movies.Business.anothers;
using Movies.Repository;
using Movies.Utilities;
using Quartz;

namespace Movies.Service;

public class QuartzService : IQuartzRepository
{
    private readonly ISchedulerFactory _schedulerFactory;
    
    public QuartzService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task<JobDetails> GetCurrentJob()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobkey = new JobKey("MovieRecordViewer");
        var triggers = await scheduler.GetTriggersOfJob(jobkey);
        var trigger = triggers.ElementAt(0);
        return new JobDetails()
        {
            JobName = jobkey.Name,
            TriggerName = trigger.Key.Name,
            JobStatus = (await scheduler.GetTriggerState(trigger.Key)).ToString(),
            Description = trigger.Description,
            NextFireTimeUtc = trigger.GetNextFireTimeUtc(),
            PreviousFireTimeUtc = trigger.GetPreviousFireTimeUtc()
        };
    }

    public async Task<string> ControlTask(int? time, string action)
    {
        action = action.ToLower();
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobkey = new JobKey("MovieRecordViewer");
        if(Constraint.JobAction.PAUSE.Equals(action))
        {
            
            await scheduler.PauseJob(jobkey);
        }
        else if(Constraint.JobAction.RESUME.Equals(action))
        {
            await scheduler.ResumeJob(jobkey);
        }
        else if(Constraint.JobAction.DELETE.Equals(action))
        {
            await scheduler.DeleteJob(jobkey);
        }
        else if (Constraint.JobAction.UPDATE.Equals(action))
        {
            if (time is null or > 7 or < 1)
            {
                return "Invalid time!";
            }
            
            DayOfWeek dayOfWeek = (DayOfWeek)Constraint.DayOfWeek.ElementAt((int)time);
            var triggers = await scheduler.GetTriggersOfJob(jobkey);
            var trigger = triggers.ElementAt(0);
            var newTrigger = trigger.GetTriggerBuilder()
                .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(dayOfWeek, 0, 0)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")))
                .Build();
            
            await scheduler.RescheduleJob(trigger.Key, newTrigger);
        }
        else
        {
            return "Invalid action!";
        }

        return "Success!";
    }
    
    public async Task<string> ExecuteJob()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobkey = new JobKey("MovieRecordViewer");
        if (await scheduler.CheckExists(jobkey))
        {
            await scheduler.TriggerJob(jobkey);
        }

        return "Success!";
    }
    
}