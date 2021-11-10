using System.Threading.Tasks;

namespace Transfer.Persistence.OpenStack.OCS.Auth
{
    internal interface IAuthManager
    {
        Task<AuthData> Authenticate();
    }
}