using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Web.Http;
using web_api.Configurations;

namespace web_api.Controllers
{
    public class MedicamentosController : ApiController
    {

        private readonly Repositories.SQLServer.Medicamento repoMedicamento; //

        public MedicamentosController() { 

            repoMedicamento = new Repositories.SQLServer.Medicamento(Database.getConnectionString());

        }

        // GET: api/Medicamentos
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(repoMedicamento.Select());
        }


        // GET: api/Medicamentos/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Models.Medicamento medicamento = repoMedicamento.Select(id);

            if (medicamento == null)
                return NotFound();

            return Ok(medicamento);
        }


        [HttpGet]
        public IHttpActionResult Get(string nome)
        {
            return Ok(repoMedicamento.Select(nome));
        }



        // POST: api/Medicamentos
        [HttpPost]
        public IHttpActionResult Post(Models.Medicamento medicamento)
        {

            if (repoMedicamento.Insert(medicamento))
                return Ok(medicamento);

            return InternalServerError();
        }




        // PUT: api/Medicamentos/5
        public IHttpActionResult Put(int id, Models.Medicamento medicamento)
        {

            if (id != medicamento.Id)
                return BadRequest("O id da requisição não coincide com o id do medicamento.");

            if (!this.repoMedicamento.Update(medicamento))
                return NotFound();

            return Ok(medicamento);
        }

        // DELETE: api/Medicamentos/5
        public IHttpActionResult Delete(int id)
        {

            if (!this.repoMedicamento.Delete(id))
                return NotFound();

            return Ok();
        }
    }
}
