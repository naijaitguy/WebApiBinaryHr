using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Models
{
    public class ApplicationModel
    {

        /// <summary>
        /// ///////////////////////
        /// </summary>
        public bool ShortListed { get; set; }


        public bool Interviewed { get; set; }


        public bool Tested { get; set; }

        public string ApplicationDate { get; set; }
        public bool Hired { get; set; }

        public bool ManagedBy { get; set; }
    }
}
