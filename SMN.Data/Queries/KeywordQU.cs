using SMN.Data.DBModels;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMN.Data.Queries
{
    public class KeywordQU : BaseData
    {
        public KeywordQU(string connectionString) : base(connectionString) { }

        public List<Keyword> GetKeywords()
        {
            var query = (
                from keyword in Context.Keyword
                select new Keyword
                {
                    KeywordId = keyword.KeywordId,
                    Value = keyword.Value
                }
                );
            return query.ToList();

        }

        public async Task<Keyword> PostKeywordAsync(Keyword keyword)
        {
            if (KeywordValueExists(keyword.Value) == true)
            {
                throw new System.ArgumentException("Keyword already exists!", "Keyword Exist");
            }
            else
            {
                Context.Keyword.Add(keyword);
                await Context.SaveChangesAsync();
                return keyword;
            }
        }

        public List<Keyword> GetTopKeywords(int userId)
        {
            var list = new List<Keyword>();
            var newUser = Context.User.Include(x => x.Category).FirstOrDefault(m => m.UserId == userId);

            var Topics = (from topic in Context.Topic
                        from topicCategory in Context.ThreadCategory
                        from topicRole in Context.TopicRole
                        from userRole in Context.UserRole
                        from topicKeyword in Context.TopicKeyword
                        where ((topic.TopicId == topicCategory.Topic.TopicId)
                        && (topic.Locked == true)
                        && (topicCategory.Category.Department == newUser.Category.Department || topicCategory.Category.CategoryId == 1)
                        && (topic.TopicId == topicRole.Topic.TopicId)
                        && (userRole.User.UserId == newUser.UserId)
                        && (topicRole.Role.Type == userRole.Role.Type)
                        && (topic.TopicId == topicKeyword.Topic.TopicId))
                        select new TopicKeyword
                        {
                            TopicKeywordId = topicKeyword.TopicKeywordId,
                            Topic = topicKeyword.Topic,
                            Keyword = topicKeyword.Keyword
                        }
                        );
            var topicKeywordsByCategoryByRole = Topics.ToList();


            var mostUsedKeywordsByCategoryByRole = topicKeywordsByCategoryByRole
                                    .GroupBy(q => q.Keyword)
                                    .OrderByDescriptionending(gp => gp.Count())
                                    .Take(5)
                                    .Select(g => g.Key).ToList();

            return mostUsedKeywordsByCategoryByRole;
        }

        private bool KeywordValueExists(string value)
        {
            return Context.Keyword.Any(e => e.Value == value);
        }

     
    }
}