using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public class AcademicServices :IAcademicServices
    {

        private BinaryHrDbContext Db;
        public AcademicServices(BinaryHrDbContext _db) => this.Db = _db;
    }
}
