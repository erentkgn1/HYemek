using HepsiYemek.Entities;
using HepsiYemek.Services.Abstract;
using HepsiYemek.Services.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Services.Concrete
{
    public class ProductService : ServiceBase, IProductService
    {  
      
        public ProductService(IOptions<MongoDBSettings> options, ISendEndpointProvider sendEndpointProvider) : base(options, sendEndpointProvider)
        {
            
        }

        public bool Delete(string id)
        {
            _productCollection.DeleteOne(x => x.ID == id);
            return true;
        }


        //public async Task SendRabbitMQ(string phoneNo, string otpValue)
        //{

        //    var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:register-sms-service"));
        //    var msg = "Doğrulama kodunuz : " + otpValue;
        //    var data = new SendOTPValueMessageCommand(msg, phoneNo, Utilities.CreateSecVal("taXI2021", new string[] { msg, phoneNo }));
        //    await sendEndpoint.Send<SendOTPValueMessageCommand>(data);

        //}


        public ICollection<Product> GetList( string name)
        {
            var query = _productCollection.AsQueryable();

            
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            var list = query.ToList();

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Category = _categoryCollection.Find(x => x.CategoryId == item.CategoryId).FirstOrDefault();
                }
            }

            return list;
        }

        public Product Get(string _id)
        {
            var product = _productCollection.AsQueryable().FirstOrDefault(x=>x.ID == _id);

            if (product != null)
            {
                product.Category = _categoryCollection.Find(x => x.CategoryId == product.CategoryId).FirstOrDefault();
            }
          
            return product;
        }

        public bool Insert(Product product)
        {
            try
            {
                _productCollection.InsertOne(product);
              
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                _productCollection.ReplaceOne(x => x.ID == product.ID, product);
                this.SendRabbitMQ("update-product", product.ID, "");
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public ICollection<Product> GetListByCategoryID(string catId)
        {
            var list = _productCollection.AsQueryable().Where(x => x.CategoryId == catId).ToList();
            return list;
        }
    }
}
