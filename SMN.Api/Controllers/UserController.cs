using SMN.Business;
using SMN.Data.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMN.Api.Controllers
{
  
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserController(IConfiguration configuration) : base(configuration) { }

        [HttpGet("{EDU}")]
        public User GetUser([FromRoute] string EDU)
        {
            using (var userQU = new UserQU(ConnectionString))
            {
                return userQU.GetUser(EDU);
            }
        }


        [Route("GetUsersByDepartment/{department}")]
        public List<User> GetUsersByDepartment([FromRoute] string department)
        {
            using (var userQU = new UserQU(ConnectionString))
            {
                return userQU.GetUsersByDepartment(department);
            }
        }


        [HttpPut]
        public async Task<User> PutUser([FromBody] User user)
        {
            try
            {
                using (var userQU = new UserQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        Badrequest(ModelState);
                    }

                    await userQU.PutUser(user);
                    return user;
                }
                            
            }
            catch (Exception)
            {
                return user;
            }
        }

    }
}
