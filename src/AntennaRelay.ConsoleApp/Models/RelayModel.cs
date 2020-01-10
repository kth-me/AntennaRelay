using System.Collections.Generic;

namespace AntennaRelay.ConsoleApp.Models
{
    public class RelayModel
    {
        public List<Dictionary<string, Dictionary<string, string>>> Relay { get; set; } = new List<Dictionary<string, Dictionary<string, string>>>()
        {
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "FirstRelay", new Dictionary<string, string>()
                    {
                        {"Source", "457063331518349315"},
                        {"Destination", "650599729348083722"}
                    }
                }
            },
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "SecondRelay", new Dictionary<string, string>()
                    {
                        {"Source", "665034602448158731"},
                        {"Destination", "665034625185349632"}
                    }
                }
            },
            new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "ThirdRelay", new Dictionary<string, string>()
                    {
                        {"Source", "457063331518349315"},
                        {"Destination", "665034625185349632"}
                    }
                }
            }
        };
    }
}