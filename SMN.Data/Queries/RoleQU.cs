using SMN.Data.DBModels;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SMN.Data.Queries
{
    public class RoleQU : BaseData
    {
        public RoleQU(string connectionString) : base(connectionString) { }

        public Role GetRole(string EDU)
        {
            var query = (
                from role in Context.Role
                from user in Context.User
                from userRole in Context.UserRole
                where ((user.UserId == userRole.User.UserId && role.RoleId == userRole.Role.RoleId && user.EDU == EDU))
                select new Role
                {
                    RoleId = role.RoleId,
                    Type = role.Type
                }
                ).FirstOrDefault();
            return query;
        }

        public List<Role> GetRoles()
        {
            var query = (from role in Context.Role
                         select role);
            return query.ToList();
        }
    }
}
