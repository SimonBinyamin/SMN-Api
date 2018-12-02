using SMN.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMN.Data.Queries
{
    public class TopicCategoryQU : BaseData
    {
        public TopicCategoryQU(string connectionString) : base(connectionString) { }

        public List<TopicCategory> GetTopicCategorys(int topicId)
        {
            var query = (
                from topicCategory in Context.TopicCategory
                where ((topicCategory.Topic.TopicId == topicId))
                select new TopicCategory
                {
                    TopicCategoryId = topicCategory.TopicCategoryId,
                    Topic = topicCategory.Topic,
                    Category = topicCategory.Category
                }
                    );
            return query.ToList();
        }

        public async Task PostTopicCategoryAsync(TopicCategory topicCategory)
        {
            TopicCategory newTopicCategory = new TopicCategory
            {
                TopicCategoryId = topicCategory.TopicCategoryId,
                Topic = Context.Topic.First(x => x.TopicId == topicCategory.Topic.TopicId),
                Category = Context.Category.First(x => x.CategoryId == topicCategory.Category.CategoryId)
            };

            if (TopicCategoryExists(newTopicCategory.Category.Department, newTopicCategory.Topic.TopicId) == false)
            {
                Context.TopicCategory.Add(newTopicCategory);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Category already linked");
            }
        }


        public async Task DeleteTopicCategoryAsync(int tTopicCategoryId)
        {
            var topicCategory = await Context.TopicCategory.SingleOrDefaultAsync(m => m.TopicCategoryId == topicCategoryId);
            Context.TopicCategory.Remove(topicCategory);
            await Context.SaveChangesAsync();
        }

        private bool TopicCategoryExists(string department, int topicId)
        {
            var topic = (from topicCategory in Context.TopicCategory.Include(ap => ap.Category)
                                                           .Include(ap => ap.Topic)
                       where topicCategory.Category.Department == department
                           && topicCategory.Topic.TopicId == topicId
                       select topicCategory);

            return topic.Any();

        }

    }
}
