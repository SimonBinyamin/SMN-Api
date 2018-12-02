using SMN.Data.DBModels;
using System.Collections.Generic;
using System.Linq;

namespace SMN.Data.Queries
{
    public class CategoryQU : BaseData
    {
        public CategoryQU(string connectionString) : base(connectionString) { }

        public List<Category> GetCategorys(bool includePublic = false)
        {
            var query = (from category in Context.Category
                         select category);
            return query.ToList();
        }
    }
}
