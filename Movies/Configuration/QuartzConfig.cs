using Movies.Quartz;
using Movies.Repository;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using WatchDog;

namespace Movies.Configuration
{
    public static class QuartzConfig 
    {

        public static void AddQuartzConfig(this IServiceCollection services)
        {

            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";
                q.SchedulerName = "Quartz Scheduler";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 5;
                });

            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
                options.AwaitApplicationStarted = true;
            });

            services.ConfigureOptions<QuartzSettting>();
            services.AddSingleton<StdSchedulerFactory>();

        }
    }
}
