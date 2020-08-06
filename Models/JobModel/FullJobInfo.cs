using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Models.JobModel
{
    public class FullJobInfo
    {


        public Job Job { get; set; }

        public Application JobApplications { get; set; }


        public Experience workExperience { get; set; }

        public  Academic ApplicantAcademic { get; set; }

        public string ExpiringDate { get; set; }
        public string PostedDate { get; set; }

    }
}
