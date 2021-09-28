using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.Services.Concrete
{
    public class RedisService
    {
        private string _host { get; set; }
        private int _port { get; set; }

        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDB(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
