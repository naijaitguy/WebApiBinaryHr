using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Models.JobModel
{
    public class CreateJobModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string JobTitle { get; set; }


        public Guid JobId { get; set; }

        [Required]
        public DateTime ExpiringDate { get; set; }
        

        public string ExpiringDateInString { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Discription { get; set; }


        [Required]
        public string JobType { get; set; }

          public DateTime PostedDate { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string YearsOfExperience { get; set; }



       

    }
}
