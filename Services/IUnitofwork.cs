using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Services
{
   public interface IUnitofwork
    {

        public Task CommitAsync();
        public IJobServices Jobservice
        { get; }

        public IApplicationServices  Applicationservice
        { get; }


        public IProfileServices Profileservice
        { get; }


        public IUserServices Userservice
        { get; }


        public IExperienceServices JExperienceservices
        { get; }


        public IAcademicServices AcademicServices { get; }


    }
}
