using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Identity;

namespace HelloJobBackEnd.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
