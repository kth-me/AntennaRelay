namespace AntennaRelay.ConsoleApp.Models
{
    // Structure of config.json file
    public class ConfigModel
    {
        public string Token { get; set; }
        public string Playing { get; set; }
        public string FirstChannelId { get; set; }
        public string SecondChannelId { get; set; }
    }
}