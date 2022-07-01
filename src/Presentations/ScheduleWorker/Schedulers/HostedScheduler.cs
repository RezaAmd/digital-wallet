using Quartz;
using Quartz.Spi;
using ScheduleWorker.Models;

namespace ScheduleWorker.Schedulers
{
    public class HostedScheduler : IHostedService
    {
        #region Dependenct Injection
        public IScheduler scheduler { get; set; }

        private readonly JobDto _jobDto;
        private readonly IJobFactory _jobFactory;
        private readonly ISchedulerFactory _schedulerFactory;

        public HostedScheduler(JobDto jobDto,
            IJobFactory jobFactory,
            ISchedulerFactory schedulerFactory)
        {
            _jobDto = jobDto;
            _jobFactory = jobFactory;
            _schedulerFactory = schedulerFactory;
        }
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create scheduler.
            scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            scheduler.JobFactory = _jobFactory;
            // Create job.
            IJobDetail jobDetail = CreateJob(_jobDto);
            //Create trigger.
            ITrigger trigger = CreateTrigger(_jobDto);
            // Schedule job.
            await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            // Start the scheduler.
            await scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobDto jobDto)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobDto.Id.ToString())
                .WithDescription(jobDto.Name)
                .WithCronSchedule(jobDto.CronExpression)
                .Build();
        }

        private IJobDetail CreateJob(JobDto jobDto)
        {
            return JobBuilder.Create(jobDto.Type)
                .WithIdentity(jobDto.Id.ToString())
                .WithDescription(jobDto.Name)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await scheduler.Shutdown();
        }
    }
}