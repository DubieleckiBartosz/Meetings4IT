using Microsoft.AspNetCore.Builder;
using Panels.Infrastructure.Jobs;
using Quartz;

namespace Panels.Infrastructure.Configurations;

public static class QuartzConfiguration
{
    public static WebApplicationBuilder PanelsQuartzConfiguration(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("UpdateMeetingStatusJob");

            // Register the job with the DI container
            q.AddJob<UpdateMeetingStatusJob>(opts => opts.WithIdentity(jobKey));

            // Create a trigger for the job
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("UpdateMeetingStatusJob-trigger")
                .WithCronSchedule("0 0 * * * ?")); // run every 5 seconds "0/5 * * * * ?" for tests
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return builder;
    }
}