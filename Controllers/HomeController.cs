using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Models.JobModel;
using WebApiBinaryHr.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiBinaryHr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IUnitofwork Services_Repo;
        private readonly ILogger <HomeController> logger;

        public HomeController(IUnitofwork unitofwork, ILogger<HomeController> _logger)
        { this.Services_Repo = unitofwork;
            logger = _logger;
        }
        // GET: api/<HomeController>
        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<ActionResult<List<Job>>>GetAllJobs()
        {

            try {
                
                var Result = await this.Services_Repo.Jobservice.GetAllJobs();

                if (Result.Count() > 0) { return Ok(Result); }

                else {

                    logger.LogError("NO JOB FOUND ");
                    return NotFound(new { Mgs = " No Job Found " }) ;

                    
                }
            }

            catch ( Exception Ex)
            {
                logger.LogError(Ex.ToString());
                return BadRequest( new { Mgs = "Internal Server Err" } ); }
          
        }

        // GET api/<HomeController>/5
        // GET api/<JobController>/5
        [HttpGet]
        [Route("GetJobbyid/{id}")]
        public async Task<ActionResult<CreateJobModel>> Get(Guid id)
        {

            // Guid ID = new Guid(id);
            if (id == null) { return BadRequest("Id is required "); }

            try
            {

                var Result = await this.Services_Repo.Jobservice.GetJobById(id);

                if (Result == null) { return NotFound("No Job Found"); }
                else
                {

                    return Ok(Result);
                }


            }
            catch (Exception Ex)
            {

                return BadRequest("internal server error !" + Ex);

            }


        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
