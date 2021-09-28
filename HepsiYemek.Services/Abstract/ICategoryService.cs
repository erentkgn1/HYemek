using HepsiYemek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Services.Abstract
{
    public interface ICategoryService
    {
        ICollection<Category> GetList( string name);
        Category Get(string _id);
        bool Update(Category cat);
        bool Insert(Category cat);
        bool Delete(string id);

    }
}
