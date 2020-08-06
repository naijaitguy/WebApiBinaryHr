using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Models
{
    public class Emailmodel
    {


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
