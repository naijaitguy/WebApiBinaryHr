using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
   public  interface IApplicationServices
    {

        public Task<List<Application>> GetAllApplications();
        public Task<Application> GetApplicationById(Guid ApplicationID);

        public Task<bool> CheckDuplicateApplication(Guid JobId, Guid UserId);

        public Task<bool> RemoveApplication(Application Model, Guid ApplicationId);
        public Task<bool> AddApplication(Application Model);
    }
}
