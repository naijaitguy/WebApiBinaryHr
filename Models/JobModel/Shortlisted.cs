using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Models.JobModel
{
    public class Shortlisted
    {

        public Job Job { get; set; }

       

        public Application JobApplications { get; set; }
    }
}
