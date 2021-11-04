using System.Threading.Tasks;

namespace Parser
{
    public interface IParserBook
    {
        void Parse();
        BookInfo GetResult();
    }
}