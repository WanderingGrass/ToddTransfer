using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Persistence.Lock
{
    public class RedisOptions
    {
        public string ConnectionString { get; set; } = "localhost";
        public string Instance { get; set; }
        public int Database { get; set; }
    }
}
