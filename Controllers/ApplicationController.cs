using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiBinaryHr._Helper;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiBinaryHr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {

        private IUnitofwork Repo;
        public ApplicationController(IUnitofwork iunitofwork) => this.Repo = iunitofwork;

        // GET api/<ApplicationController>/5
        [HttpGet]
        [Route("Apply/{id}")]
        [Authorize(Roles ="User")]
        [Produces("Application/json")]
        public async Task<ActionResult<string>> Get(Guid id)
        {
           
            if(id == null) { return BadRequest("Job Id is required"); }

            string CurrentUserId = ExtentionHelper.GetUserId(User);
            Guid UserId = new Guid(CurrentUserId);
            try {

                var job = await this.Repo.Jobservice.CheckJobExpiringDate(id);

                if (job == true) { return BadRequest("Job Already Expired"); }

                var applicationexist = await this.Repo.Applicationservice.CheckDuplicateApplication(id, UserId);
                if (applicationexist == true) { return BadRequest("Application Already Exist"); }
                else
                {

                    Application NewAppl = new Application
                    {

                        UserId = CurrentUserId,
                        JobId = id,
                        Created_At = DateTime.Now,

                    };

                   

                    var Result = await this.Repo.Applicationservice.AddApplication(NewAppl);

                    if (Result == true) {

                        var Updatejobresult = await this.Repo.Jobservice.UpdateTotalJobApplication(id);
                        if (Result == true) {

                            await this.Repo.CommitAsync();
                            return Ok("Application Successful !");

                        } else { return BadRequest("Internal server Error !"); }

                          
                    
                    }

                    else { return BadRequest("Internal server Error !"); }
                    
                    

                }



            
            }
            catch(Exception Ex) { return BadRequest("internal Server Error !"); }



        }

     


    }
}
