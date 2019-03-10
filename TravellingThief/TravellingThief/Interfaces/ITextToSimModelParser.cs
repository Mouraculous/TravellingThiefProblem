using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface ITextToSimModelParser
    {
        ISimulationParameters Parse(string[] text);
    }
}
