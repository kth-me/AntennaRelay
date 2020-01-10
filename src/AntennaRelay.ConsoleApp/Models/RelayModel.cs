using System.Collections.Generic;

namespace AntennaRelay.ConsoleApp.Models
{
    public class RelayModel
    {
        public static List<Dictionary<string, Dictionary<string, string>>> Relay { get; set; } = new List<Dictionary<string, Dictionary<string, string>>>()
        {
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "RelayOne", new Dictionary<string, string>()
                    {
                        {"SourceChannelId", ""},
                        {"DestinationChannelId", ""}
                    }
                }
            },
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "RelayTwo", new Dictionary<string, string>()
                    {
                        {"SourceChannelId", ""},
                        {"DestinationChannelId", ""}
                    }
                }
            },
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "RelayThree", new Dictionary<string, string>()
                    {
                        {"SourceChannelId", ""},
                        {"DestinationChannelId", ""}
                    }
                }
            }
        };
    }
}