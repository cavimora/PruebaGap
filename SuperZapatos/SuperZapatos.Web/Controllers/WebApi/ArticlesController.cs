using SuperZapatos.BL;
using SuperZapatos.DTO.Response;
using SuperZapatos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuperZapatos.Web.Controllers.WebApi
{
    [BasicAuthentication]
    public class ArticlesController : ApiController
    {
        private ArticlesBL _bl;
        // GET: api/articles
        public Result<ComplexArticles<List<ArticleDto>>> Get()
        {
            _bl = new ArticlesBL();
            return _bl.Get();
        }

        // GET: api/articles/5
        public Result<ComplexArticles<ArticleDto>> Get(int id)
        {
            _bl = new ArticlesBL();
            return _bl.Get(id);
        }

        // GET: api/Stores/Store/5
        [Route("api/articles/store/{id}")]
        public Result<ComplexArticles<List<ArticleDto>>> GetByStore(int id)
        {
            _bl = new ArticlesBL();
            return _bl.GetByStore(id);
        }

        // POST: api/articles
        public Result<ArticleDto> Post([FromBody]ArticleDto value)
        {
            _bl = new ArticlesBL();
            return _bl.Add(value);
        }

        // PUT: api/articles/5
        public Result<ArticleDto> Put(int id, [FromBody]ArticleDto value)
        {
            _bl = new ArticlesBL();
            return _bl.Update(value);
        }

        // DELETE: api/articles/5
        public Result<bool> Delete(int id)
        {
            _bl = new ArticlesBL();
            return _bl.Delete(id);
        }
    }
}
