﻿using Application.Dao;
using Application.Interfaces.Context;
using Quartz;

namespace ScheduleWorker.Jobs
{
    public class DepositStateJob : IJob
    {
        #region Initializing
        private readonly ILogger<DepositStateJob> _logger;
        public DepositStateJob(ILogger<DepositStateJob> logger
            )
        {
            _logger = logger;
        }
        #endregion

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Deposit state run at: {DateTime.Now.ToString("HH:mm:ss")} and job type: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}