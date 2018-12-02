using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMN.Data.DBModels;
using SMN.Business;
using System.Net;
using System.Linq;

namespace SMN.Api.Controllers.TopicsList
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        public TopicController(IConfiguration configuration) : base(configuration)
        {
        }

        [Route("GetTopicsByIds/{topicIds}")]
        public List<Topic> GetTopicsByIds([FromRoute] string topicIds)
        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                return topicQU.GetTopicsByIds(topicIds);
            }
        }

        [Route("GetTopicsPerDepartment/{department}")]
        public List<Topic> GetTopicsPerDepartment([FromRoute] string department)

        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                return topicQU.GetTopicsPerDepartment(department);
            }
        }

        [Route("GetTopicsPerUserId/{userId}")]
        public List<Topic> GetTopicsPerUserId([FromRoute] int userId)

        {
            using (var topicQU = new TopicQU(ConnectionString))
            {

                return topicQU.GetTopicsPerUserId(userId);
            }
        }

        [Route("getTopicsByUserIdByKeywordId/{userId}/{keywordId}")]
        public List<Topic> GetTopicsByUserIdByKeywordId([FromRoute] int userId, [FromRoute] int keywordId)

        {
            using (var topicQU = new TopicQU(ConnectionString))
            {

                return topicQU.GetTopicsByUserIdByKeywordId(userId, keywordId);
            }
        }

        [Route("GetTopicBrowsePerTopicId/{topicId}")]
        public Topic GetTopicBrowsePerTopicId([FromRoute] int topicId)
        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                return topicQU.GetTopicBrowsePerTopicId(topicId);
            }
        }

        [Route("GetNonActiveTopics")]
        public List<Topic> GetNonActiveTopics()
        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                return topicQU.GetNonActiveTopics();
            }
        }

        [HttpPut("{topicId}")]
        public async Task PutTopic([FromRoute] int topicId, [FromBody] Topic topic)
        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                if (!ModelState.IsValid)
                {
                    Badrequest(ModelState);
                }

                await topicQU.PutTopic(topicId, topic);
            }
        }


        [HttpPost]
        public async Task<Topic> PostTopic([FromBody] Topic topic)
        {

            try
            {

                using (var topicQU = new TopicQU(ConnectionString))
                {
                    if (!ModelState.IsValid || topic == null)
                    {
                        Badrequest(ModelState);
                    }
                    return await topicQU.PostTopicAsync(topic);
                }

            }
            catch (Exception)
            {
                return topic;
            }

        }

        [HttpGet("{topicId}")]
        public Topic GetTopic([FromRoute] int topicId)
        {
            using (var topicQU = new TopicQU(ConnectionString))
            {
                return topicQU.GetTopic(topicId);
            }
        }


        [HttpDelete("{topicId}")]
        public async Task<IActionResult> DeleteTopic([FromRoute] int topicId)
        {
            try
            {
                using (var topicQU = new TopicQU(ConnectionString))
                {
                    if (!ModelState.IsValid)
                    {
                        Badrequest(ModelState);
                    }

                    await topicQU.DeleteTopic(topicId);
                    return Ok("Topic deleted!");
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please unlink the Topic from its CategoryS!");
            }

        }

    }
}
