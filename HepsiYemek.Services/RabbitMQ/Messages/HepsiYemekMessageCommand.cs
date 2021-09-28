using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.Services.RabbitMQ.Messages
{
    public class HepsiYemekMessageCommand
    {
        public string productId { get; set; }
        public string categoryId { get; set; }

        public HepsiYemekMessageCommand(string productId, string categoryId)
        {
            this.productId = productId;
            this.categoryId = categoryId;
        }

       
    }
}
