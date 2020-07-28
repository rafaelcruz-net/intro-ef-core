using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private AlunoServices Services { get; set; }

        public AlunoController(AlunoServices services)
        {
            this.Services = services;
        }


        [HttpGet]
        public IEnumerable<Aluno> Get()
        {
            return Services.GetAll();
        }

        [HttpGet("{id}")]
        public Aluno Get(Guid id)
        {
            return Services.GetAlunoById(id);
        }

        [HttpPost]
        public void Post([FromBody] Aluno model)
        {
            try
            {
                this.Services.Save(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this.Services.Delete(id);
        }
    }
}
