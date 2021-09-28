using HepsiYemek.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Services.Abstract
{
    public interface IProductService
    {
        ICollection<Product> GetList(string name);
        ICollection<Product> GetListByCategoryID(string catId);
        Product Get(string _id);
        bool Update(Product product);
        bool Insert(Product product);
        bool Delete(string id);
    }
}
