using System.Threading.Tasks;

namespace AntennaRelay.ConsoleApp
{
    internal class Program
    {
        private static Task Main()
            => new Client().InitializeAsync();
    }
}