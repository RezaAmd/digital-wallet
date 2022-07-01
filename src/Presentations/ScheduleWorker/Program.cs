using DigitalWallet.Application;
using DigitalWallet.Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using ScheduleWorker.JobFactories;
using ScheduleWorker.Jobs;
using ScheduleWorker.Models;
using ScheduleWorker.Schedulers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddInfrastructure();
        services.AddRepositoryServices();

        services.AddSingleton<IJobFactory, JobFactory>()
        .AddSingleton<ISchedulerFactory, StdSchedulerFactory>()
        .AddSingleton<DepositStateJob>()
        .AddSingleton(new JobDto("Deposit State", typeof(DepositStateJob), "0/30 * * * * ?"))
        .AddHostedService<HostedScheduler>()
        ;
    })
    .Build();

await host.RunAsync();