using System.Collections.Generic;
using SMN.Data.DBModels;
using System.Linq;
using System.Threading.Tasks;

namespace SMN.Data.Queries
{
    public class TopicCellphoneQU : BaseData
    {
        public TopicCellphoneQU(string connectionString) : base(connectionString) { }
        public List<TopicCellphone> GetTopicCellphones(int topicId)
        {
            var query = (from topicCellphone in Context.TopicCellphone
                         where topicCellphone.Topic.TopicId == topicId
                         select new TopicCellphone
                         {
                             TopicCellphoneId = topicCellphone.TopicCellphoneId,
                             Topic = topicCellphone.Topic,
                             Cellphone = topicCellphone.Cellphone
                         }
                      );
            return query.ToList();
        }

        public async Task EditTopicCellphone(int topicId, Topic topic)
        {
            await DeleteTopicCellphonesByTopicId(topicId);

            await AddTopicCellphone(topicId, topic);

        }

        public async Task AddTopicCellphone(int topicId, Topic topic)
        {
            foreach (var cellphone in topic.Cellphones)
            {
                TopicCellphone topicCellphone = new TopicCellphone
                {
                    Topic = Context.Topic.First(x => x.TopicId == topicId),
                    Cellphone = Context.Cellphone.First(x => x.CellphoneId == cellphone.CellphoneId)

                };
                Context.TopicCellphone.Add(topicCellphone);
            }
            await Context.SaveChangesAsync();
        }

        public async Task DeleteTopicCellphonesByTopicId(int topicId)
        {
            var toDelete = (from a in Context.TopicCellphone
                            where a.Topic.TopicId == TopicId
                            select a);
            if (toDelete.Any())
            {
                Context.TopicCellphone.RemoveRange(toDelete);
                await Context.SaveChangesAsync();
            }
        }
    }
}
