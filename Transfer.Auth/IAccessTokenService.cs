using System.Threading.Tasks;

namespace Transfer.Auth
{
    public interface IAccessTokenService
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}