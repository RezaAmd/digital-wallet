namespace DigitalWallet.ScheduleWorker.Models
{
    public class JobDto
    {
        public JobDto(string name, Type type, string cronExpression)
        {
            Name = name;
            Type = type;
            CronExpression = cronExpression;
        }

        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public Type Type { get; set; }
        public string CronExpression { get; set; }
    }
}