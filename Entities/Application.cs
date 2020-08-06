using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime Created_At { get; set; }
    }
}
