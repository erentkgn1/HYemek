using HepsiYemek.Services.Abstract;
using HepsiYemek.Services.Concrete;
using HepsiYemek.Services.RabbitMQ.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Services.RabbitMQ.Consumers
{
    public class HepsiYemekMessageCommandComsumer : IConsumer<HepsiYemekMessageCommand>
    {
        public RedisService _redisService;
        public IProductService _productService;
        public HepsiYemekMessageCommandComsumer(RedisService redisService, IProductService productService)
        {
            _redisService = redisService;
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<HepsiYemekMessageCommand> context)
        {

            if (!string.IsNullOrEmpty(context.Message.productId))
            {
              await  _redisService.GetDB().KeyDeleteAsync(context.Message.productId);
            }
            else if (!string.IsNullOrEmpty(context.Message.categoryId))
            {
                var list = _productService.GetListByCategoryID(context.Message.categoryId);
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        await _redisService.GetDB().KeyDeleteAsync(item.ID);
                    }
                }
            }


        }
    }
}
