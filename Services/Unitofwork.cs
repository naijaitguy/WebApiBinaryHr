using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public class Unitofwork:IUnitofwork
    {

        private readonly BinaryHrDbContext DbCon;

        private  IJobServices IJob;
        private IApplicationServices GetApplicationServices;
        private IProfileServices GetProfileServices;
        private IUserServices GetUserServices;
        private IAcademicServices GetAcademicServices;
        private IExperienceServices GetExperienceServices;




        public Unitofwork(BinaryHrDbContext _Db) => this.DbCon = _Db;



        public IJobServices Jobservice { 
            
            get{

                if (this.IJob == null)
                {

                    this.IJob = new JobServices(DbCon);
                }

                return this.IJob;
            }
        
        }

        public IApplicationServices Applicationservice
        { get {

                if (this.GetApplicationServices == null)
                {

                    this.GetApplicationServices = new  ApplicationServices (DbCon);
                }

                return this.GetApplicationServices;

            } }


        public IProfileServices Profileservice
        { get {

                if(this.GetProfileServices == null) { this.GetProfileServices = new ProfileServices(this.DbCon); }
                return this.GetProfileServices;
            
            } }


        public IUserServices Userservice
        { get {
            
                if(this.GetUserServices == null)
                {

                    this.GetUserServices = new UserServices(this.DbCon); 
                }

                return this.GetUserServices;
            
            } }


        public IExperienceServices JExperienceservices
        { get {
            
            if(this.GetExperienceServices == null) { this.GetExperienceServices = new ExperienceServices(this.DbCon); }
                return this.GetExperienceServices;
            
            } }


        public IAcademicServices AcademicServices
        { get {

                if (this.GetAcademicServices == null)
                { this.GetAcademicServices = new AcademicServices(this.DbCon); }

                return this.GetAcademicServices;
             } }

        public async Task CommitAsync() {

          await  this.DbCon.SaveChangesAsync();

        }
    }
}
