using SMN.Data.DBModels;
using System.Collections.Generic;
using System.Linq;

namespace SMN.Data.Queries
{
    public class CellphoneQU : BaseData
    {
        public CellphoneQU(string connectionString) : base(connectionString) { }
        public List<Cellphone> GetCellphones()
        {
            var query = (from cellphone in Context.Cellphone
                         select new Cellphone
                         {
                             CellphoneId = cellphone.CellphoneId,
                             Name = cellphone.Name,
                             PhoneNumber = cellphone.PhoneNumber
                         }
       );
            return query.ToList();
        }

    }
}
