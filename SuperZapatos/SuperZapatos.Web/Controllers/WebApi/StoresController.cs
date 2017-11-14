using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperZapatos.DTO;
using SuperZapatos.DTO.Response;
using SuperZapatos.BL;

namespace SuperZapatos.Web.Controllers.WebApi
{
    [BasicAuthentication]
    public class StoresController : ApiController
    {

        private StoresBL _bl;
        // GET: api/Stores
        public Result<List<StoreDto>> Get()
        {
            _bl = new StoresBL();
            return _bl.Get();
        }

        // GET: api/Stores/5
        public Result<StoreDto> Get(int id)
        {
            _bl = new StoresBL();
            return _bl.Get(id);
        }

        // POST: api/Stores
        public Result<StoreDto> Post([FromBody]StoreDto value)
        {
            _bl = new StoresBL();
            return _bl.Add(value);
        }

        // PUT: api/Stores/5
        public Result<StoreDto> Put(int id, [FromBody]StoreDto value)
        {
            _bl = new StoresBL();
            return _bl.Update(value);
        }

        // DELETE: api/Stores/5
        public Result<bool> Delete(int id)
        {
            _bl = new StoresBL();
            return _bl.Delete(id);
        }
    }
}
