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
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IOptions<MongoDBSettings> options, ISendEndpointProvider sendEndpointProvider) : base(options, sendEndpointProvider)
        {
        }

        public bool Delete(string id)
        {
            _categoryCollection.DeleteOne(x => x.CategoryId == id);
            return true;
        }

        public ICollection<Category> GetList(string name)
        {
            var query = _categoryCollection.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            var list = query.ToList();
            return list;
        }

        public Category Get(string _id)
        {
            var category = _categoryCollection.AsQueryable().FirstOrDefault(x => x.CategoryId == _id);

            return category;
        }

        public bool Insert(Category cat)
        {
            try
            {
                _categoryCollection.InsertOne(cat);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }

        public bool Update(Category cat)
        {
            try
            {
                _categoryCollection.ReplaceOne(x => x.CategoryId == cat.CategoryId, cat);
                this.SendRabbitMQ("update-product", "", cat.CategoryId);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
