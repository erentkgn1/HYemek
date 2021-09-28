using HepsiYemek.API.Models;
using HepsiYemek.Entities;
using HepsiYemek.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Text.Json;

namespace HepsiYemek.API.Filters
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        public RedisService _redisService;
        
        public RedisCacheAttribute(RedisService redisService)
        {
            _redisService = redisService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            try
            {
                var id = context.RouteData.Values["_id"].ToString();

                if (!string.IsNullOrEmpty(id))
                {
                    var existingProduct = _redisService.GetDB().StringGet(id);

                    if (existingProduct.HasValue)
                    {
                        context.Result = new OkObjectResult(new ServiceResponse(JsonSerializer.Deserialize<Product>(existingProduct)));
                    }

                    else
                    {
                        base.OnActionExecuting(context);
                    }

                }
                else
                {
                    base.OnActionExecuting(context);
                }

            }
            catch 
            {

                base.OnActionExecuting(context); 
            }



        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var result = (OkObjectResult)context.Result;
                var serviceResponse = (ServiceResponse)result.Value;

                if (serviceResponse != null && serviceResponse.isSuccess == true && serviceResponse.Data != null)
                {
                    var product = (Product)serviceResponse.Data;
                    if (product != null)
                    {
                        _redisService.GetDB().StringSet(product.ID, JsonSerializer.Serialize(product), expiry: TimeSpan.FromMinutes(5));

                    }
                }
            }
            catch 
            {

               
            }

            base.OnActionExecuted(context);
        }
    }

}
