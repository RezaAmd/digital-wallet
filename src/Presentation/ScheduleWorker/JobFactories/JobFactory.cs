using Quartz;
using Quartz.Spi;

namespace ScheduleWorker.JobFactories
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob? NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return _serviceProvider.GetService(jobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}