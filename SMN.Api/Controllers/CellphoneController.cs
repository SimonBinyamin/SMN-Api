using SMN.Business;
using SMN.Data.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SMN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CellphoneController : Controller
    {
        public CellphoneController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet]
        public List<Cellphone> GetCellphones()
        {
            using (var cellphoneQU = new CellphoneQU(ConnectionString))
            {
                return cellphoneQU.GetCellphones();
            }
        }

    }
}
