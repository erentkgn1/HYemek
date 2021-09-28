using HepsiYemek.Entities;
using HepsiYemek.Services.RabbitMQ.Messages;
using HepsiYemek.Services.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Services.Abstract
{
    public abstract class ServiceBase
    {

        public IMongoCollection<Product> _productCollection { get; set; }
        public IMongoCollection<Category> _categoryCollection { get; set; }
        public MongoDBSettings _databaseSettings;

        private ISendEndpointProvider _sendEndpointProvider;

        protected ServiceBase(IOptions<MongoDBSettings> options, ISendEndpointProvider sendEndpointProvider)
        {
            _databaseSettings = options.Value;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var db = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = db.GetCollection<Product>("products");
            _categoryCollection = db.GetCollection<Category>("categories");
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendRabbitMQ(string uri, string productId, string categoryId)
        {

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:"+uri+""));
          
            var data = new HepsiYemekMessageCommand(productId, categoryId);
            await sendEndpoint.Send<HepsiYemekMessageCommand>(data);

        }

    }
}
