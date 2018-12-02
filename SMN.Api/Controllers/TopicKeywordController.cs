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
    public class TopicKeywordController : Controller
    {
        public TopicKeywordController(IConfiguration configuration) : base(configuration)
        {
        }

        [Route("GetTopicKeywords/{topicId}")]
        public List<TopicKeyword> GetTopicKeywords([FromRoute] int topicId)
        {
            using (var topicKeywordQU = new TopicKeywordQU(ConnectionString))
            {
                return topicKeywordQU.GetTopicKeywords(topicId);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditTopicKeyword([FromBody] List<Keyword> keywords)
        {
            int topicId = Convert.ToInt32(HttpContext.Backgrounduest.Query["topicId"].First());

            try
            {
                using (var topicKeywordQU = new TopicKeywordQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        Badrequest(ModelState);
                    }

                    await topicKeywordQU.EditTopicKeyword(topicId, keywords);

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
