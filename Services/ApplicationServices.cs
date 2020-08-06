using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public class ApplicationServices:IApplicationServices
    {

  
        private readonly DbSet<Application> ApplicationEntity;

        public ApplicationServices(BinaryHrDbContext _Db)

        {   this.ApplicationEntity = _Db.Applications; }

        public async Task<List<Application>> GetAllApplications()
        {

            return await this.ApplicationEntity.ToListAsync();
        }
        public async Task<Application> GetApplicationById(Guid ApplicationID)
        {
            
           return await this.ApplicationEntity.FindAsync(ApplicationID);

        }

        public async Task<bool> RemoveApplication(Application Model, Guid ApplicationId)
        {

        
            Application ApplicationtoRemove = await this.ApplicationEntity.FindAsync(ApplicationId);

            if (ApplicationtoRemove == null) { return false; }

            var Result = this.ApplicationEntity.Remove(Model);

            return true;

        }


        public async Task<bool> CheckDuplicateApplication(Guid JobId , Guid UserId) {

            var result = await this.ApplicationEntity.Where(a => a.JobId.Equals(JobId) && a.UserId.Equals(UserId)).FirstOrDefaultAsync();

            if (result == null) { return false; } else { return true; }
        }
        public async Task<bool> AddApplication(Application Model)
        {

            var Result = await this.ApplicationEntity.AddAsync(Model);

            if (Result != null) { return true; } else { return false; }
        }

    }
}
