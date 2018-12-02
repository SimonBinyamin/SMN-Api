using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMN.Data.DBModels;
using SMN.Business;
using System.Net;

namespace SMN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TopicCellphoneController : Controller
    {
        public TopicCellphoneController(IConfiguration configuration) : base(configuration)
        {
        }

        [Route("GetTopicCellphones/{topicId}")]
        public List<TopicCellphone> GetTopicCellphones([FromRoute] int topicId)
        {
            using (var topicCellphoneQU = new TopicCellphoneQU(ConnectionString))
            {
                return topicCellphoneQU.GetTopicCellphones(topicId);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditTopicCellphone([FromBody] Topic topic)
        {
            int topicId = Convert.ToInt32(HttpContext.Backgrounduest.Query["topicId"].First());

            try
            {
                using (var topicCellphoneQU = new TopicCellphoneQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        BadBackgrounduest(ModelState);
                    }

                    await topicCellphoneQU.EditTopicCellphone(topicId, topic);

                    return Ok("Linked!");
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error!");
            }

        }

    }
}
