using System.Threading.Tasks;
using MongoDB.Driver;

namespace Transfer.Persistence.MongoDB
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}