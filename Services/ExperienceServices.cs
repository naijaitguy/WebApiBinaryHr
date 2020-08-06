using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public class ExperienceServices:IExperienceServices
    {

        private BinaryHrDbContext Db;
        public ExperienceServices(BinaryHrDbContext _db) => this.Db = _db;
    }
}
