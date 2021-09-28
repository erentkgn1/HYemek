using AutoMapper;
using HepsiYemek.API.Filters;
using HepsiYemek.API.Models;
using HepsiYemek.Entities;
using HepsiYemek.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("{_id}")]
        [ServiceFilter(typeof(RedisCacheAttribute))]
        public IActionResult Get(string _id)
        {

            var result = _productService.Get(_id);
            return Ok(new ServiceResponse(result));

        }

        [HttpGet]
        [Route("List")]

        public IActionResult List(string name)
        {


            var result = _productService.GetList(name);
            return Ok(new ServiceResponse(result));


        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Add([FromBody] AddProductRequest product)
        {

            var newProduct = _mapper.Map<Product>(product);
            var result = _productService.Insert(newProduct);
            return Ok(new ServiceResponse(result));

        }

        [HttpPut]
        [ValidateModel]
        public IActionResult Update([FromBody] UpdateProductRequest product)
        {

            var newProduct = _mapper.Map<Product>(product);
            var result = _productService.Update(newProduct);
            return Ok(new ServiceResponse(result));

        }

        [HttpDelete]
        [Route("{_id}")]
        public IActionResult Delete(string _id)
        {

            var result = _productService.Delete(_id);
            return Ok(new ServiceResponse(result));

        }
    }
}
