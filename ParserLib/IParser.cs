using System.Threading.Tasks;

namespace Parser
{
    public interface IParser<T>
    {
        T[] Parse();
        Task<T[]> ParseAsync();
    }
}