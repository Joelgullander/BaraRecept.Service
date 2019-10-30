using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaraRecept.Recipe.Api.Database;
using BaraRecept.Recipe.Api.Options;
using BaraRecept.Recipe.Api.Services;
using BaraRecept.Recipe.Contracts.Entities;
using BaraRecept.Recipe.Contracts.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BaraRecept.Recipe.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API-methods for keywords
    /// </summary>
    public class KeywordController : BaseController
    {

        /// <summary>
        /// Creates new instance of KeywordController
        /// </summary>


        /// <summary>
        /// Gets mocked values
        /// </summary>
        /// <remarks>
        /// Shows how to get values from options, DI etc.
        /// </remarks>
        /// <returns>An object of mocked data</returns>
        ///
        [HttpPost]
        [Route("", Name = "PostKeyword")]
        public async Task<IActionResult> PostKeyword([FromBody] DbKeyword keyword)
        {
            using (var db = new DatabaseContext())
            {
                var keywords = db.Keywords;
                var foundKeyword = keywords.FirstOrDefault(x => x.KeywordName.ToLower() == keyword.KeywordName.ToLower());

                // If we find a match we don't want to create a duplicate.
                if(foundKeyword != null)
                {
                    return Ok(foundKeyword);
                }

                await keywords.AddAsync(keyword);

                db.SaveChanges();

                return Ok(keyword);
            }
        }

        [HttpGet]
        [Route("", Name = "GetPopularKeywords")]
        public IActionResult GetPopularKeywords([FromQuery] string search)
        {
            using (var db = new DatabaseContext())
            {
                var keywords = db.Keywords;

                if(search != null)
                {

                    var matched = keywords.Where(s => s.KeywordName.StartsWith(search, StringComparison.InvariantCultureIgnoreCase)).Take(40);
                    if (matched == null)
                    {
                        return NotFound();
                    }

                    return Ok(matched.ToArray());
                }

                var popularKeywords = keywords.Take(20).ToArray();

                if (popularKeywords == null)
                {
                    return NotFound();
                }

                return Ok(popularKeywords);
            }
        }

        [HttpGet]
        [Route("{keywordId}", Name = "GetKeyword")]
        public async Task<IActionResult> GetRecipe([FromRoute] int keywordId)
        {
            using (var db = new DatabaseContext())
            {
                var keywords = db.Keywords;
                var keyword = await keywords.FindAsync(keywordId);

                if (keyword == null)
                {
                    return NotFound();
                }
                
                return Ok(keyword);
            }
        }

        [HttpDelete]
        [Route("{keywordId}")]
        public async Task<IActionResult> DeleteKeyword([FromRoute] int keywordId)
        {
            // Consider moving to an archive instead of permanently removing.
            using (var db = new DatabaseContext())
            {
                var keywords = db.Keywords;
                var keyword = await keywords.FindAsync(keywordId);

                if (keyword == null)
                {
                    return NotFound();
                }

                keywords.Remove(keyword);

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
