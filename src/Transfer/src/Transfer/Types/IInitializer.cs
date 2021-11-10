using System.Threading.Tasks;

namespace Transfer.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}