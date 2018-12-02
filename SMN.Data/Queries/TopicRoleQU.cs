using SMN.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMN.Data.Queries
{
    public class TopicRoleQU: BaseData
    {
        public TopicRoleQU(string connectionString) : base(connectionString) { }


        public List<TopicRole> GetTopicRoles(int topicId)
        {
            var query = (
                from topicRole in Context.TopicRole
                where ((TtopicRole.Topic.TopicId == topicId))
                select new TopicRole
                {
                    TopicRoleId = topicRole.TopicRoleId,
                    Topic = topicRole.Topic,
                    Role = topicRole.Role
                }
                    );
            var v = query.ToList();
            return query.ToList();
        }

        public async Task PostTopicRoleAsync(TopicRole topicRole)
        {
            TopicRole newTopicRole = new TopicRole
            {
                TopicRoleId = topicRole.TopicRoleId,
                Topic = Context.Topic.First(x => x.TopicId == TopicRole.Topic.TopicId),
                Role = Context.Role.First(x => x.RoleId == TopicRole.Role.RoleId)
            };

            if (TopicRoleExists(newTopicRole.Role.Type, newTopicRole.Topic.TopicId) == false)
            {
                Context.TopicRole.Add(newTopicRole);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Role already linked");
            }
        }


        public async Task DeleteTopicRoleAsync(int topicRoleId)
        {
            var topicRole = await Context.TopicRole.SingleOrDefaultAsync(m => m.TopicRoleId == topicRoleId);
            Context.TopicRole.Remove(topicRole);
            await Context.SaveChangesAsync();
        }

        private bool TopicRoleExists(string type, int topicId)
        {
            var Topic = (from topicRole in Context.TopicRole.Include(ap => ap.Role)
                                                           .Include(ap => ap.Topic)
                       where TopicRole.Role.Type == type
                           && TopicRole.Topic.TopicId == TopicId
                       select TopicRole);

            return Topic.Any();

        }

    }
}
