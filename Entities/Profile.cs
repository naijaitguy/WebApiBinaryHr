using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class Profile
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual Academic Academics { get; set; }
        public virtual Experience Experience { get; set; }
        public string ExpectedSalary { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Cv { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
        public string LGA { get; set; }
        public string Address { get; set; }



    }
}
