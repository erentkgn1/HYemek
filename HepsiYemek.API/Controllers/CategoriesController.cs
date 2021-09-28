using AutoMapper;
using HepsiYemek.API.Filters;
using HepsiYemek.API.Models;
using HepsiYemek.Entities;
using HepsiYemek.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HepsiYemek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{_id}")]
        public IActionResult Get(string _id)
        {

            var b = 0;
            var a = 30 / b;
            var result = _categoryService.Get(_id);
            return Ok(new ServiceResponse(result));

        }

        [HttpGet]
        [Route("List")]

        public IActionResult List(string name)
        {


            var result = _categoryService.GetList(name);
            return Ok(new ServiceResponse(result));


        }

       
        [HttpPost]
        [ValidateModel]
        public IActionResult Add([FromBody] AddCategoryRequest cat)
        {
         

            var newProduct = _mapper.Map<Category>(cat);
            var result = _categoryService.Insert(newProduct);
            return Ok(new ServiceResponse(result));

        }

        [HttpPut]
        [ValidateModel]
        public IActionResult Update([FromBody] UpdateCategoryRequest cat)
        {

            var newCat = _mapper.Map<Category>(cat);
            var result = _categoryService.Update(newCat);
            return Ok(new ServiceResponse(result));

        }

       
        [HttpDelete]
        [Route("{_id}")]
        public IActionResult Delete(string _id)
        {

            var result = _categoryService.Delete(_id);
            return Ok(new ServiceResponse(result));

        }
    }
}
