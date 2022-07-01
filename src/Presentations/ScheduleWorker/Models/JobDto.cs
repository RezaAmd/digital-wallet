namespace ScheduleWorker.Models
{
    public class JobDto
    {
        public JobDto(string name, Type type, string cronExpression)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Type = type;
            CronExpression = cronExpression;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string CronExpression { get; set; }
    }
}