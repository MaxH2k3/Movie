using Microsoft.Extensions.Options;
using Movies.Utilities;
using Quartz;

namespace Movies.Quartz
{
    public class QuartzSettting : IConfigureOptions<QuartzOptions>
    {

        public void Configure(QuartzOptions options)
        {
            /*var jobkey = JobKey.Create("MovieRecordViewer");
            options.AddJob<QuartzTask>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                .AddTrigger(trigger => trigger.ForJob(jobkey)
                    .WithSimpleSchedule(schedule => 
                        schedule.WithIntervalInSeconds(Constraint.JobTime.Time).RepeatForever()));*/
            
            var jobkey = JobKey.Create("MovieRecordViewer");
            options.AddJob<QuartzTask>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                .AddTrigger(trigger => trigger.ForJob(jobkey)
                    .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Friday, 0, 0)
                        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")))
                    .WithDescription("Calculate sum of all movie a weakly. Transfer to another database and then reset the current database.")
                    .Build());
        }
    }
}