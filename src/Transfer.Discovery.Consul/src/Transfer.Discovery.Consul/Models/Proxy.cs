using System.Collections.Generic;

namespace Transfer.Discovery.Consul.Models
{
    public class Proxy
    {
        public List<Upstream> Upstreams { get; set; }
    }
}