
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace AikoLearning.Infrastructure.Data.Jobs;

public class JobScheduler : Registry
{
    public JobScheduler(IServiceProvider serviceProvider)
    {
        Schedule(() =>
        {
            var statsJobScheduler = serviceProvider.GetRequiredService<StatsJobScheduler>();
            statsJobScheduler.Execute();
        }).ToRunNow().AndEvery(1).Minutes();
    }

    public void ScheduleJobs()
    {
        JobManager.Initialize(this);
    }
}