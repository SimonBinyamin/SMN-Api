using SMN.Data.DBModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SMN.Data.Queries
{
    public class UserRoleQU : BaseData
    {
        public UserRoleQU(string connectionString) : base(connectionString) { }


        public async Task PutUserRoleAsync(UserRole userRole)
        {

            if (!(UserRoleExists(userRole)))
            {
                await PostUserRoleAsync(userRole);
            }
            else
            {
                throw new Exception("Role already linked");
            }
        }

        public async Task PostUserRoleAsync(UserRole userRole)
        {
            UserRole newUserRole = new UserRole
            {
                UserRoleId = userRole.UserRoleId,
                User = Context.User.First(u => u.UserId == userRole.User.UserId),
                Role = Context.Role.First(r => r.RoleId == userRole.Role.RoleId)
            };
            Context.UserRole.Add(newUserRole);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(int userRoleId)
        {
            var userRole = await Context.UserRole.SingleOrDefaultAsync(m => m.UserRoleId == userRoleId);
            Context.UserRole.Remove(userRole);
            await Context.SaveChangesAsync();
        }

        public List<UserRole> GetUserRoleByUserId(int userId)
        {
            var query = (from userRole in Context.UserRole
                         where (userRole.User.UserId == userId)
                         select new UserRole
                         {
                             UserRoleId = userRole.UserRoleId,
                             User = userRole.User,
                             Role = userRole.Role

                         }
                );
            var a = query.ToList();
            return query.ToList();
        }

        private bool UserRoleExists(UserRole userRole)
        {
            var userRoleQU = (from uRole in Context.UserRole.Include(ap => ap.Role)
                                               .Include(ap => ap.User)
                              where uRole.Role.RoleId == userRole.Role.RoleId
                                  && uRole.User.UserId == userRole.User.UserId
                              select uRole);

            return userRoleQU.Any();
        }
    }
}
