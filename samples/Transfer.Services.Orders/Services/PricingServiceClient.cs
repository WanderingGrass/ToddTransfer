using System;
using System.Threading.Tasks;
using Transfer.HTTP;
using Transfer.Secrets.Vault;
using Transfer.WebApi.Security;
using Transferor.Services.Orders.DTO;

namespace Transferor.Services.Orders.Services
{
    public class PricingServiceClient : IPricingServiceClient
    {
        private readonly IHttpClient _client;
        private readonly string _url;

        public PricingServiceClient(IHttpClient client, ICertificatesService certificatesService,
            HttpClientOptions httpClientOptions, VaultOptions vaultOptions, SecurityOptions securityOptions)
        {
            _client = client;
            _url = httpClientOptions.Services["pricing"];
            if (!vaultOptions.Enabled || vaultOptions.Pki?.Enabled != true ||
                securityOptions.Certificate?.Enabled != true)
            {
                return;
            }
            
            var certificate = certificatesService.Get(vaultOptions.Pki.RoleName);
            if (certificate is null)
            {
                return;
            }

            var header = securityOptions.Certificate.GetHeaderName();
            var certificateData = certificate.GetRawCertDataString();
            _client.SetHeaders(h => h.Add(header, certificateData));
        }

        public Task<PricingDto> GetAsync(Guid orderId)
            => _client.GetAsync<PricingDto>($"{_url}/orders/{orderId}/pricing");
    }
}
