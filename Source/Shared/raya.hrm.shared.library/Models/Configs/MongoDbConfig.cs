namespace Raya.Hrm.Shared.Library.Models.Configs
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ErrorCollection { get; set; } = "error_logs";
        public string RequestCollection { get; set; } = "request_logs";
    }
}
