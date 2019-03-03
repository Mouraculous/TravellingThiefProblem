using System.Collections.Generic;

namespace TravellingThief.Interfaces
{
    public interface ITextDataProvider
    {
        IEnumerable<string> Get();
    }
}
