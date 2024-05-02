using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using web_api.Configurations;

namespace web_api.Controllers
{
    public class ExamesController : ApiController
    {

        private readonly Repositories.SQLServer.Exame repoExame;

        public ExamesController() {

            repoExame = new Repositories.SQLServer.Exame(Database.getConnectionString());
        
        }



        // GET: api/Exames
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(repoExame.select());
        }



        // GET: api/Exames/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {

            Models.Exame exame = repoExame.select_(id);

            if(exame == null)
                return NotFound();

            return Ok(exame);
        }





        // POST: api/Exames
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Exames/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Exames/5
        public void Delete(int id)
        {
        }
    }
}
