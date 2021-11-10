using System.Threading.Tasks;
using MongoDB.Driver;

namespace Transfer.Persistence.MongoDB
{
    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}