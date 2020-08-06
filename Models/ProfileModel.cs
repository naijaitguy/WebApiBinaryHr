using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Models
{
    public class ProfileModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

 
        [Required]
        public string Cv { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public string YearsOfExperience { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string NyscYear { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Salary { get; set; }
        [Required]
        public string AlternativePhone { get; set; }
        [Required]
        public string Notice { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        public string LGA { get; set; }
        [Required]
        public string State { get; set; }

     

    
   

    }
}
