using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiBinaryHr._Helper;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Models.JobModel;
using WebApiBinaryHr.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiBinaryHr.Controllers
{
    [Route("api/[controller]")]
    [Produces("Application/json")]
    [ApiController]
    [Authorize(Roles = "Admin,Director,Supervirsor")]
    public class JobController : ControllerBase
    {
        private readonly IUnitofwork Repo;
        public JobController(IUnitofwork unitofwork) => this.Repo = unitofwork;
      
        // GET api/<JobController>/5
        [HttpGet]
        [Route("GetJobbyid/{id}")]
        public async  Task< ActionResult< CreateJobModel>> Get(Guid id)
        {

           // Guid ID = new Guid(id);
            if (id == null) { return BadRequest("Id is required "); }

            try {

                var Result = await this.Repo.Jobservice.GetJobById(id);

                if (Result == null) { return NotFound("No Job Found"); } else {

                    return Ok(Result);
                }


            }
            catch (Exception Ex)
            {

                return BadRequest("internal server error !"+Ex);
            
            }


        }

        // POST api/<JobController>
        [HttpPost]
        [Route("AddJob")]
        public async Task<ActionResult<bool>> Post([FromBody] CreateJobModel Model)
        {

            if (!ModelState.IsValid) { return BadRequest("Model Error"); }

            try {

                string CurrentUserId = ExtentionHelper.GetUserId(User);
                Job NewJob = new Job
                {
                    CompanyName = Model.CompanyName,
                    Created_At = DateTime.Now,
                    Description = Model.Discription,
                    Experince = Model.YearsOfExperience,
                    Expired_At =  Model.ExpiringDate,
                    Location = Model.Location,
                    Title = Model.JobTitle,
                    JobType = Model.JobType,
                    Qualification = Model.Qualification,
                    UserId = CurrentUserId


                };

                
                var Result = await this.Repo.Jobservice.AddJob(NewJob);

                if (Result == true) {

                   await Repo.CommitAsync();
                    return Ok(" Job Successfully Added !"); }

                else { return BadRequest("fail to add job"); }
            
            
            }
            catch (Exception Ex)
            {

                return BadRequest("Internal server error !");
            }

        }

        // PUT api/<JobController>/5
        [HttpPut]
        [Route("UpdateJob/{id}")]
        public async Task<ActionResult<bool>> Put([FromBody] CreateJobModel Model, Guid Id)
        {
          
            if (Id == null) { return BadRequest("Id is Required"); }

            if (Id != Model.JobId) { return BadRequest("Id is Required"); }

            try {
                Job NewJob = new Job
                {
                    CompanyName = Model.CompanyName,
                    Created_At = Model.PostedDate,
                    Description = Model.Discription,
                    Experince = Model.YearsOfExperience,
                    Expired_At = Model.ExpiringDate,
                    Location = Model.Location,
                    Title = Model.JobTitle,
                    JobType = Model.JobType,
                    Qualification = Model.Qualification,
                  


                };


                var Result =  this.Repo.Jobservice.UpdateJob(NewJob, Id);

                if (Result == null) { return BadRequest("Update fail"); }
                else {
                    await Repo.CommitAsync();
                    return Ok("Update Successful"); }
            
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }

        }

        // DELETE api/<JobController>/5
        [HttpDelete]
        [Route("DeleteJob/{id}")]
        public async Task<ActionResult<bool>> Delete([FromBody] CreateJobModel Model, Guid Id)
        {

            
            if (Id == null) { return BadRequest("Id is Required"); }

            if (Id != Model.JobId) { return BadRequest("Id is Required"); }

            try {

                var result = await this.Repo.Jobservice.RemoveJob(Id);

                if (result == true) {

                  await  Repo.CommitAsync();
                    return Ok(" Job Successfully Removed"); } else { return BadRequest("Could not Delete Job"); }
            }
            catch (Exception ex)
            { return BadRequest("Internal server error"); }
        }
    }



}
