using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Models;
using WebApiBinaryHr.Models.JobModel;

namespace WebApiBinaryHr.Services
{
   public interface IJobServices
    {

        public Task<List<CreateJobModel>> GetAllJobs();
        public Task<CreateJobModel> GetJobById( Guid JobID);

        public Task<bool> RemoveJob(Guid JobId);
        public Task<bool> AddJob( Job Model);
        public Task<bool> UpdateJob(Job Model, Guid Id);
        public  Task<bool> CheckJobExpiringDate(Guid Id);
        public Task<bool> UpdateTotalJobApplication(Guid Id);


    }
}
