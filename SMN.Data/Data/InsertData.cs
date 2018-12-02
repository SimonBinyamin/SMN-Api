using SMN.Data.DBModels;
using System.Linq;

namespace SMN.Data.Data
{
    public static class InsertInitialData
    {
        public static void InsertCategorysData(this SMNContext context)
        {
            context.Database.EnsureCreated();
            context.Category.Add(new Category { Department = "Health" });
            context.Category.Add(new Category { Department = "Sport" });
            context.SaveChanges();
        }

        public static void InsertUsersData(this SMNContext context)
        {
            context.Database.EnsureCreated();
            context.User.Add(
             new User { FirstName = "Simon", LastName = "Binyamin", EDU = "Id", Email = "simonbinyamin@gmail.com" }
            );
           
            context.SaveChanges();
        }


        public static void InsertRolesData(this SMNContext context)
        {
            context.Database.EnsureCreated();

            context.Role.Add(
                new Role { Type = "Admin" }
                );

            context.Role.Add(
               new Role { Type = "User" }
               );

            context.Role.Add(
                new Role { Type = "Controller" }
                );

            context.SaveChanges();
        }


        public static void InsertUserrolesData(this SMNContext context)
        {
            context.Database.EnsureCreated();

      
                context.UserRole.Add(new UserRole
                {
                    User = context.User.First(x => x.UserId == 1),
                    Role = context.Role.First(x => x.RoleId == 3)
                });

            
            context.SaveChanges();
        }

        public static void InsertBrowseValues(this SMNContext context)
        {
            context.Database.EnsureCreated();
            context.Browse.Add(
                new Browse { BrowseId = 1, Value = "Chrome" }
                );

            context.Browse.Add(
                new Browse { BrowseId = 2, Value = "IE" }
                );

            context.Browse.Add(
                new Browse { BrowseId = 3, Value = "Opera" }
                );
            context.SaveChanges();
        }

        public static void InsertCellphoneData(this SMNContext context)
        {

            context.Database.EnsureCreated();
            context.Cellphone.Add(
                new Cellphone { Name = "Al Cellphone", PhoneNumber = "08" }
                );
            context.SaveChanges();
        }

    }
}
