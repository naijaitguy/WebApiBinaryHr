using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Models;
using WebApiBinaryHr.Models.JobModel;

namespace WebApiBinaryHr.Services
{
    public class JobServices :IJobServices
    {


        private readonly DbSet<Job> JobEntity;

        public JobServices(BinaryHrDbContext _Db)

        { this.JobEntity = _Db.Jobs; }

    

        public async Task<bool> RemoveJob(Guid JobId) {

         //   Guid Id = new Guid(JobId);

            
            Job JobtoRemove = await this.JobEntity.FindAsync(JobId);

            if(JobtoRemove == null) { return false; }

            var Result =  this.JobEntity.Remove(JobtoRemove);

            return true;

        }
        public async Task<bool> AddJob(Job Model) {

            var Result = await this.JobEntity.AddAsync(Model);

            if (Result != null) { return true; } else { return false; }
        }

        public async Task<List<CreateJobModel>> GetAllJobs()
        {

            // return await this.Entity.Jobs.ToListAsync();

            var Jobs = await (from a in this.JobEntity
                              orderby a.Created_At descending
                              select new CreateJobModel
                              {
                                  CompanyName = a.CompanyName,
                                  Discription = a.Description,
                                  ExpiringDateInString = a.Expired_At.ToString("dd-MMM-yyyy"),
                                  JobId = a.Id,
                                  JobTitle = a.Title,
                                  JobType = a.JobType,
                                  Location = a.Location,
                                  Qualification = a.Qualification,
                                  YearsOfExperience = a.Experince,
                                  PostedDate = a.Created_At

                              }).ToListAsync();

            return Jobs;

        }


        public async Task<bool> CheckJobExpiringDate(Guid Id)
        {

            DateTime Date = DateTime.Now;
         //   Guid JobId = new Guid(Id);


            var job = await this.JobEntity.Where(j => j.Id == Id && j.Expired_At < Date).SingleOrDefaultAsync();
            if (job != null) { return true; } else { return false; }



        }

        public async Task<CreateJobModel> GetJobById(Guid Id)
        {

          //  Guid JobId = new Guid(Id);

            var Job = await (from a in JobEntity
                             where a.Id == Id
                             select new CreateJobModel
                             {

                                 CompanyName = a.CompanyName,
                                 Discription = a.Description,
                                 ExpiringDateInString = a.Expired_At.ToString("dd-MMM-yyyy"),
                                 JobId = a.Id,
                                 
                                 ExpiringDate = a.Expired_At,
                                 JobTitle = a.Title,
                                 JobType = a.JobType,
                                 Location = a.Location,
                                 Qualification = a.Qualification,
                                 YearsOfExperience = a.Experince,
                                 PostedDate = a.Created_At



                             }).SingleOrDefaultAsync();
            if (Job == null) { return null; } else { return Job; }

           

        }



        public async Task<bool> UpdateJob(Job Model, Guid Id)
        {

            try
            {

              //  Guid JobId = new Guid(Id);
                Job Job = await this.JobEntity.FindAsync(Id);
                if (Job == null) { return false; }
                else
                {

                    Job.CompanyName = Model.CompanyName;
                    Job.Description = Model.Description;
                    Job.Expired_At = Model.Expired_At;
                  
                    Job.Title = Model.Title;
                    Job.JobType = Model.JobType;
                    Job.Location = Model.Location;
                    Job.Qualification = Model.Qualification;
                  
                    Job.Experince = Model.Experince;
                    Job.Total = Model.Total;
                    Job.Hired = Model.Hired;
                    Job.Interview = Model.Interview;
                    Job.Pending = Model.Pending;
                    Job.Rejected = Model.Rejected;
                    Job.ShortListed = Model.ShortListed;
                   



                    return true;


                }

            }
            catch (DbUpdateException)
            {
                return false;
            }


        }



        public async Task<bool> UpdateTotalJobApplication( Guid Id)
        {

            try
            {

                //  Guid JobId = new Guid(Id);
                Job Job = await this.JobEntity.FindAsync(Id);
                if (Job == null) { return false; }
                else
                {

                   ;
                    Job.Total += 1;
                 

                    return true;


                }

            }
            catch (DbUpdateException)
            {
                return false;
            }


        }



    }
}
