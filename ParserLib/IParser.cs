using System.Threading.Tasks;

namespace Parser
{
    public interface IParser
    {
        void Parse();
        BookInfo GetResult();
    }
}