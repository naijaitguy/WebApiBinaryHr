using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class Academic
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public string Institution1 { get; set; }
        public string Institution2 { get; set; }
        public string Institution3 { get; set; }

        public string Course1 { get; set; }
        public string Course2 { get; set; }
        public string Course3 { get; set; }

        public string Deg_Class1 { get; set; }
        public string Deg_Class2 { get; set; }
        public string Deg_Class3 { get; set; }

        public string Qualification1 { get; set; }
        public string Qualification2 { get; set; }
        public string Qualification3 { get; set; }

        public DateTime Started_At1 { get; set; }
        public DateTime Started_At2 { get; set; }
        public DateTime Started_At3 { get; set; }

        public DateTime Ended_At1 { get; set; }
        public DateTime Ended_At2 { get; set; }
        public DateTime Ended_At3 { get; set; }
    }
}
