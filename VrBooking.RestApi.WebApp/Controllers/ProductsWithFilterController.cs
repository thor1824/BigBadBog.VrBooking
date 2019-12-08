using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsWithFilterController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;


        public ProductsWithFilterController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public ActionResult<List<FilterPageProductList>> Post([FromBody] FilterPageProductList pagefilter)
        {
            try
            {
                
                return Ok(_productService.ReadAllWithPageFilter(pagefilter));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}