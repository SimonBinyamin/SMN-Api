using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMN.Data.DBModels;
using SMN.Business;
using System.Threading.Tasks;
using System;

namespace SMN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class KeywordController : Controller
    {
        public KeywordController(IConfiguration configuration) : base(configuration) { }

        [HttpGet]
        public List<Keyword> GetKeywords()
        {
            using (var keywordQU = new KeywordQU(ConnectionString))
            {
                return keywordQU.GetKeywords();
            }
        }


        [Route("getTopKeywordsByUserId/{userId}")]
        public List<Keyword> GetTopKeywordsByUserId(int userId)
        {
            using (var keywordQU = new KeywordQU(ConnectionString))
            {
                return keywordQU.GetTopKeywords(userId);
            }
        }


        [HttpPost]
        public async Task<Keyword> PostKeyword([FromBody] Keyword Keyword)
        {
            try
            {
                using (var keywordQU = new KeywordQU(ConnectionString))
                {
                    if (!ModelState.IsValid || keyword == null)
                    {
                        BadRequest(ModelState);
                    }

                    await keywordQU.PostKeywordAsync(keyword);
                    return await keywordQU.PostKeywordAsync(keyword);
                }

            }
            catch (Exception)
            {
                return keyword;
            }

        }

    }

}