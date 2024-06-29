using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthExample.domain.Entities
{
    public class User : IdentityUser<int>
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
