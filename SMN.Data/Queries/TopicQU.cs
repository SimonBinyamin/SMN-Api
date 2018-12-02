using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SMN.Data.DBModels;

namespace SMN.Data.Queries
{
    public class TopicQU : BaseData
    {

        TopicCellphoneQU TopicCellphoneQU;
        public TopicQU(string connectionString) : base(connectionString)
        {
            TopicCellphoneQU = new TopicCellphoneQU(connectionString);
        }

        public List<Topic> GetTopicsPerDepartment(string department)
        {
            var query = (from topic in Context.Topic
                         from topicCategory in Context.ThreadCategory
                         where ((topic.TopicId == topicCategory.Topic.TopicId) && (topic.Locked == true) && (topicCategory.Category.Department == department || topicCategory.Category.CategoryId == 1))
                         select new Topic
                         {
                             TopicId = topic.TopicId,
                             Name = topic.Name,
                             Background = topic.Background,
                             Description = topic.Description,
                             Route = topic.Route,
                             VideoRoute = topic.VideoRoute,
                             Picture = topic.Picture,
                             PostDate = topic.PostDate,
                             TopicroveDate = topic.TopicroveDate,
                             Poster = Context.User.First(x => x.UserId == topic.Poster.UserId),
                             Topicrover = Context.User.First(x => x.UserId == topic.Topicrover.UserId),
                             Browse = Context.Browse.First(x => x.Value == topic.Browse.Value)
                         }
           );
            var DistinctList = query.ToList().GroupBy(i => i.TopicId).Select(g => g.First()).ToList();
            return DistinctList;
        }

        public List<Topic> GetTopicsByUserIdByKeywordId(int userId, int keywordId)
        {
            var newUser = Context.User.Include(x => x.Category).FirstOrDefault(m => m.UserId == userId);


            var query = (from topic in Context.Topic
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
                         && (topic.TopicId == topicKeyword.Topic.TopicId)
                         && (topicKeyword.Keyword.KeywordId == KeywordId))
                         select new Topic
                         {
                             TopicId = topic.TopicId,
                             Name = topic.Name,
                             Background = topic.Background,
                             Description = topic.Description,
                             Route = topic.Route,
                             VideoRoute = topic.VideoRoute,
                             Picture = topic.Picture,
                             PostDate = topic.PostDate,
                             TopicroveDate = topic.TopicroveDate,
                             Poster = Context.User.First(x => x.UserId == topic.Poster.UserId),
                             Topicrover = Context.User.First(x => x.UserId == topic.Topicrover.UserId),
                             Browse = Context.Browse.First(x => x.Value == topic.Browse.Value)
                         }
                        );
            return query.ToList().GroupBy(i => i.TopicId).Select(g => g.First()).ToList();


        }

        public List<Topic> GetTopicsPerUserId(int userId)
        {

            var newUser = Context.User.Include(x => x.Category).FirstOrDefault(m => m.UserId == userId);


            var query = (from topic in Context.Topic
                         from topicCategory in Context.ThreadCategory
                         from topicRole in Context.TopicRole
                         from userRole in Context.UserRole
                         where ((Topic.TopicId == TopicCategory.Topic.TopicId)
                         && (Topic.Locked == true) 
                         && (TopicCategory.Category.Department == newUser.Category.Department || TopicCategory.Category.CategoryId == 1)
                         && (Topic.TopicId == TopicRole.Topic.TopicId) 
                         && (userRole.User.UserId == newUser.UserId) 
                         && (TopicRole.Role.Type == userRole.Role.Type))
                         select new Topic
                         {
                             TopicId = topic.TopicId,
                             Name = topic.Name,
                             Background = topic.Background,
                             Description = topic.Description,
                             Route = topic.Route,
                             VideoRoute = topic.VideoRoute,
                             Picture = topic.Picture,
                             PostDate = topic.PostDate,
                             TopicroveDate = topic.TopicroveDate,
                             Poster = Context.User.First(x => x.UserId == Topic.Poster.UserId),
                             Topicrover = Context.User.First(x => x.UserId == Topic.Topicrover.UserId),
                             Browse = Context.Browse.First(x => x.Value == Topic.Browse.Value)
                         }
                        );
            var DistinctList = query.ToList().GroupBy(i => i.TopicId).Select(g => g.First()).ToList();
            return DistinctList;
        }

        public Topic GetTopicBrowsePerTopicId(int topicId)
        {
            var query = (
                from topic in Context.Topic
                where (Topic.TopicId == topicId)
                select new Topic
                {
                    Browse = Context.Browse.First(x => x.Value == topic.Browse.Value)
                }
                ).FirstOrDefault();
            return query;
        }


        public List<Topic> GetTopicsByIds(string topicIds)
        {
            List<int> topicIdsList = TopicIds.Split(',').Select(Int32.Parse).ToList();

            var query = (

           from topic in Context.Topic
           where (TopicIdsList.Contains(Topic.TopicId))
           select new Topic
           {
               TopicId = topic.TopicId,
               Name = topic.Name,
               Background = topic.Background,
               Description = topic.Description,
               Route = topic.Route,
               VideoRoute = topic.VideoRoute,
               Picture = topic.Picture,
               PostDate = topic.PostDate,
               Poster = Context.User.First(x => x.UserId == Topic.Poster.UserId),
               Browse = Context.Browse.First(x => x.Value == Topic.Browse.Value)
           }
           );

            return query.ToList();
        }

        public List<Topic> GetNonActiveTopics()
        {
            var query = (from topic in Context.Topic
                         where (topic.Locked == false)
                         select new Topic
                         {
                             TopicId = topic.TopicId,
                             Name = topic.Name,
                             Background = topic.Background,
                             Description = topic.Description,
                             Route = topic.Route,
                             VideoRoute = topic.VideoRoute,
                             Picture = topic.Picture,
                             PostDate = topic.PostDate,
                             Poster = Context.User.First(x => x.UserId == Topic.Poster.UserId),
                             Browse = Context.Browse.First(x => x.Value == Topic.Browse.Value)

                         }
          );
            return query.ToList();
        }

        public async Task PutTopic(int topicId, Topic topic)
        {
            Topic.TopicId = topicId;
            Topic.Topicrover = Context.User.First(x => x.EDU == topic.Topicrover.EDU);
            Topic.Browse = Context.Browse.First(x => x.Value == topic.Browse.Value);
            Context.Entry(Topic).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(TopicId))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TopicExists(int topicId)
        {
            return Context.Topic.Any(e => e.TopicId == topicId);
        }


        public async Task<Topic> PostTopicAsync(Topic topic)
        {

            Topic newTopic = new Topic
            {
                TopicId = topic.TopicId,
                Name = topic.Name,
                Background = topic.Background,
                Description = topic.Description,
                Route = topic.Route,
                VideoRoute = topic.VideoRoute,
                Picture = topic.Picture,
                Locked = topic.Locked,
                PostDate = topic.PostDate,
                Poster = Context.User.First(x => x.EDU == Topic.Poster.EDU),
                Browse = Context.Browse.First(x => x.Value == Topic.Browse.Value)
            };

            Context.Topic.Add(newTopic);
            await Context.SaveChangesAsync();
            await TopicCellphoneQU.AddTopicCellphone(newTopic.TopicId, topic);

            return newTopic;

        }


        public async Task DeleteTopic(int topicId)
        {
            await TopicCellphoneQU.DeleteTopicCellphonesByTopicId(topicId);
            var Topics = await Context.Topic.SingleOrDefaultAsync(m => m.TopicId == TopicId);
            Context.Topic.Remove(Topics);
            await Context.SaveChangesAsync();
        }


        public Topic GetTopic(int topicId)
        {
            var query = (
             from topic in Context.Topic
             where ((Topic.TopicId == topicId))
             select new Topic
             {
                 TopicId = topic.TopicId,
                 Name = topic.Name,
                 Background = topic.Background,
                 Description = topic.Description,
                 Route = topic.Route,
                 VideoRoute = topic.VideoRoute,
                 Picture = topic.Picture,
                 PostDate = topic.PostDate,
                 Poster = Context.User.First(x => x.UserId == Topic.Poster.UserId),
                 Browse = Context.Browse.First(x => x.Value == Topic.Browse.Value)
             }
          ).FirstOrDefault();
            return query;
        }


    }
}
