using System.Threading.Tasks;
using Transfer.Discovery.Consul.Models;

namespace Transfer.Discovery.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<ServiceAgent> GetAsync(string name);
    }
}