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
    public class TopicRoleController : Controller
    {
        public TopicRoleController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet("{topicId}")]
        public List<TopicRole> GetTopicRoles([FromRoute] int topicId)
        {
            using (var topicRoleQU = new TopicRoleQU(ConnectionString))
            {
                return topicRoleQU.GetTopicRoles(topicId);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostTopicRole([FromBody] TopicRole topicRole)
        {
            try
            {
                using (var topicRoleQU = new TopicRoleQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        Badrequest(ModelState);
                    }

                    await topicRoleQU.PostTopicRoleAsync(topicRole);

                    return Ok("Role linked");
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Role already linked");
            }
        }


        [HttpDelete("{topicRoleId}")]
        public async Task DeleteTopicRole([FromRoute] int topicRoleId)
        {
            using (var topicRoleQU = new TopicRoleQU(ConnectionString))
            {
                if (!ModelState.IsValid)
                {
                    Badrequest(ModelState);
                }

                await topicRoleQU.DeleteTopicRoleAsync(topicRoleId);
            }
        }
    }
}
