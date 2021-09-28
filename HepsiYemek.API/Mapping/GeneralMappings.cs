using AutoMapper;
using HepsiYemek.API.Models;
using HepsiYemek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Mapping
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings()
        {

            CreateMap<Product, AddProductRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();
            CreateMap<Category, AddCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();


        }

    }
}
