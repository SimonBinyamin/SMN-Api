using System.Threading.Tasks;
using SMN.Data.DBModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SMN.Data.Queries
{
    public class UserQU : BaseData
    {
        public UserQU(string connectionString) : base(connectionString) { }

        public User GetUser(string EDU)
        {
            var query = (
                from user in Context.User
                where (user.EDU == EDU && user.EDU != null)
                select new User
                {
                    UserId = user.UserId,
                    EDU = user.EDU,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Category = user.Category
                }
                ).FirstOrDefault();
            return query;

        }

        public List<User> GetUsersByDepartment(string department)
        {
            var query = (from user in Context.User
                         where (user.Category.Department == department)
                         select new User
                         {
                             UserId = user.UserId,
                             EDU = user.EDU,
                             Email = user.Email,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Category = user.Category
                         }
                        );

            return query.ToList();
        }

        public async Task PutUser(User user)
        {

            if (UserExists(user.EDU) == false)
            {
                user.Category = Context.Category.First(x => x.Department == user.Category.Department);
                await PostUserAsync(user);
            }
            else
            {
                if (CategoryIdExists(user.Category.Department) == false)
                {
                    user.UserId = Context.User.Where(c => c.EDU == user.EDU).Select(c => c.UserId).FirstOrDefault();
                    user.Category = Context.Category.First(x => x.Department == user.Category.Department);

                    Context.Entry(user).State = EntityState.Modified;
                    try
                    {
                        await Context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }

            }
        }

        private bool CategoryIdExists(string department)
        {
            return Context.User.Any(e => e.Category.Department == department);
        }

        public async Task PostUserAsync(User user)
        {
            User newUser = new User
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                EDU = user.EDU,
                LastName = user.LastName,
                Category = user.Category
            };
            Context.User.Add(newUser);
            await Context.SaveChangesAsync();
        }

        private bool UserExists(string EDU)
        {
            var isUserExisted = (from e in Context.User
                                 where e.EDU == EDU
                                 select e);

            return isUserExisted.Any();

        }

    }
}
