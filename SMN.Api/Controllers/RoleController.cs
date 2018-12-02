using Microsoft.AspNetCore.Mvc;
using SMN.Data.DBModels;
using SMN.Business;
using Microsoft.Extensions.Configuration;
using System;

namespace SMN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        public RoleController(IConfiguration configuration) : base(configuration) { }

        [HttpGet("{EDU}")]
        public Role GetRole([FromRoute] string EDU)
        {
            using(var roleQU = new RoleQU(ConnectionString))
            {
                return roleQU.GetRole(EDU);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var roleQU = new RoleQU(ConnectionString))
                {
                    return Ok(roleQU.GetRoles());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}