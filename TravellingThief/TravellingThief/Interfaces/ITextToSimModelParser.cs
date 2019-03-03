using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface ITextToSimModelParser
    {
        Simulation Parse(string[] text);
    }
}
