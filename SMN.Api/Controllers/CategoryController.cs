using System;
using SMN.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SMN.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        public CategoryController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            try 
            {
                using (var categoryQU = new CategoryQU(ConnectionString))
                {
                    return Ok(categoryQU.GetCategorys());
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
