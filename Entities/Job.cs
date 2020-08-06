using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class Job
    {
        [Key]
        public  Guid  Id{ get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        
            public string CompanyName { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Qualification { get; set; }
        public string Experince { get; set; }
        public DateTime Created_At { get; set; }

        public string JobType { get; set; }

        public DateTime Expired_At { get; set; }
        public string Location { get; set; }



        public int Total { get; set; }
        public int Hired { get; set; }
        public int ShortListed { get; set; }
        public int Interview { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }



    }
}
