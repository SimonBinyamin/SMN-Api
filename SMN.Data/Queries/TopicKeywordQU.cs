using System.Collections.Generic;
using SMN.Data.DBModels;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SMN.Data.Queries
{
    public class TopicKeywordQU : BaseData
    {
        public TopicKeywordQU(string connectionString) : base(connectionString) { }

        public List<TopicKeyword> GetTopicKeywords(int topicId)
        {
            var query = (from topicKeyword in Context.TopicKeyword
                         where topicKeyword.Topic.TopicId == topicId
                         select new TopicKeyword
                         {
                             TopicKeywordId = topicKeyword.TopicKeywordId,
                             Topic = topicKeyword.Topic,
                             Keyword = topicKeyword.Keyword
                         }
                      );
            return query.ToList();
        }

        public async Task EditTopicKeyword(int topicId, List<Keyword> keywords)
        {
            await DeleteTopicKeywordsByTopicId(topicId);

            await AddTopicKeyword(topicId, keywords);

        }

        public async Task AddTopicKeyword(int topicId, List<Keyword> keywords)
        {
            foreach (var keyword in keywords)
            {
                TopicKeyword topicKeyword = new TopicKeyword
                {
                    Topic = Context.Topic.First(x => x.TopicId == topicId),
                    Keyword = Context.Keyword.First(x => x.Value == keyword.Value)

                };
                Context.TopicKeyword.Add(topicKeyword);
            }
            await Context.SaveChangesAsync();
        }

        public async Task DeleteTopicKeywordsByTopicId(int topicId)
        {
            var toDelete = (from a in Context.TopicKeyword
                            where a.Topic.TopicId == topicId
                            select a);
            if (toDelete.Any())
            {
                Context.TopicKeyword.RemoveRange(toDelete);
                await Context.SaveChangesAsync();
            }
        }
    }
}
