namespace Raya.Hrm.Shared.Library.Models.Configs
{
    public record ErrorConfig
    {
        public string LogDirectory { get; set; }
        public bool LogInDb { get; set; }
        public bool LoginText { get; set; }

        public bool LogInKafka { get; set; }
        public bool LogInMongo { get; set; }

    }
}
