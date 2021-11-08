using System.Threading.Tasks;

namespace Parser
{
    public interface IParser<T>
    {
        void Parse();
        Task ParseAsync();
        T[] GetResult();
    }
}