using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class Experience
    {

        [Key]
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public string Employer1 { get; set; }
        public string Employer2 { get; set; }
        public string Employer3 { get; set; }

        public string Resposibility1 { get; set; }
        public string Resposibility2 { get; set; }
        public string Resposibility3 { get; set; }



        public string Position1 { get; set; }
        public string Position2 { get; set; }
        public string Position3 { get; set; }

        public DateTime Started_At1 { get; set; }
        public DateTime Started_At2 { get; set; }
        public DateTime Started_At3 { get; set; }

        public DateTime Ended_At1 { get; set; }
        public DateTime Ended_At2 { get; set; }
        public DateTime Ended_At3 { get; set; }
    }
}
