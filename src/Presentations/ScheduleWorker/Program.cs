using DigitalWallet.Application;
using DigitalWallet.Infrastructure;
using DigitalWallet.ScheduleWorker.JobFactories;
using DigitalWallet.ScheduleWorker.Jobs;
using DigitalWallet.ScheduleWorker.Schedulers;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using DigitalWallet.ScheduleWorker.Models;

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