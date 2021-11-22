using System.Threading.Tasks;

namespace Parser
{
    public interface IParser<T>
    {
        T[] Parse(string thingName);
        Task<T[]> ParseAsync(string thingName);
    }
}