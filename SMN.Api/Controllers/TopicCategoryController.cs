using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMN.Data.DBModels;
using Microsoft.Extensions.Configuration;
using System.Net;
using SMN.Business;

namespace SMN.Api.Controllers.SMNAdmin.TopicsCategorys
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TopicCategoryController : Controller
    {
        public TopicCategoryController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet("{topicId}")]
        public List<ThreadCategory> GetTopicCategorys([FromRoute] int topicId)
        {
            using (var topicCategoryQU = new TopicCategoryQU(ConnectionString))
            {
                return topicCategoryQU.GetTopicCategorys(topicId);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> PostTopicCategory([FromBody] TopicCategory topicCategory)
        {
            try
            {
                using (var topicCategoryQU = new TopicCategoryQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        Badrequest(ModelState);
                    }

                    await topicCategoryQU.PostTopicCategoryAsync(topicCategory);

                    return Ok("Category linked");
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Category already linked");
            }
        }


        [HttpDelete("{topicCategoryId}")]
        public async Task DeleteTopicCategory([FromRoute] int topicCategoryId)
        {
            using (var topicCategoryQU = new TopicCategoryQU(ConnectionString))
            {
                if (!ModelState.IsValid)
                {
                    Badrequest(ModelState);
                }

                await topicCategoryQU.DeleteTopicCategoryAsync(topicCategoryId);
            }
        }

    }
}