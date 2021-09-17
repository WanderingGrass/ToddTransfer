using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Persistence.Lock
{
    public interface IRedisOptionsBuilder
    {
        IRedisOptionsBuilder WithConnectionString(string connectionString);
        IRedisOptionsBuilder WithInstance(string instance);
        RedisOptions Build();
    }
}
