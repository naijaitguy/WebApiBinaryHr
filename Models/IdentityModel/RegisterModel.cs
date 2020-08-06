using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Models
{
    public class RegisterModel
    {


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


      //  [Required]
      //  [DataType(DataType.PhoneNumber)]
      //  public string PhoneNumber { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
       
        public string Role { get; set; }

      //  public string Gender { get; set; }


    }
}
