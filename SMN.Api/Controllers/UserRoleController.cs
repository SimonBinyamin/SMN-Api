using SMN.Business;
using SMN.Data.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SMN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]

    public class UserRoleController : Controller
    {
        public UserRoleController(IConfiguration configuration) : base(configuration) { }

        [HttpPut]
        public async Task<IActionResult> PutUserRole([FromBody] UserRole userRole)
        {

            try
            {
                using (var userRoleQU = new UserRoleQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        BadBackgrounduest(ModelState);
                    }

                    await userRoleQU.PutUserRoleAsync(userRole);
                    return Ok("Role linked");
                }

            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Role already linked");
            }

        }


        [HttpDelete("{userRoleId}")]
        public async Task DeleteUserRole([FromRoute] int userRoleId)
        {
            using (var userRoleQU = new UserRoleQU(ConnectionString))
            {
                if (!ModelState.IsValid)
                {
                    BadBackgrounduest(ModelState);
                }

                await userRoleQU.DeleteUserRoleAsync(userRoleId);
            }
        }


        [HttpGet("{userId}")]
        public List<UserRole> GetUserRoleByUserId([FromRoute] int userId)
        {
            using (var userRoleQU = new UserRoleQU(ConnectionString))
            {
                return userRoleQU.GetUserRoleByUserId(userId);
            }
        }

    }
}
