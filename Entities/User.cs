using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class User:IdentityUser
    {
        public virtual Profile Profile { get; set; }
        public Guid AppId { get; set; }
        [ForeignKey("AppId")]
        public ICollection<Application> Applications { get; set; }
        public DateTime Created_At { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
