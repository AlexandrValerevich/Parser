using System.Threading.Tasks;

namespace Parser
{
    public interface IParser
    {
        Task ParseAsync();
        BookInfo GetResult();
    }
}