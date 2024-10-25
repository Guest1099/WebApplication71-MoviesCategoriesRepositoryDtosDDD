using Microsoft.AspNetCore.Identity;
using System;

namespace WebApplication71.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            NormalizedName = name;
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }


        public void Update(string name)
        {
            Name = name;
        }
    }
}
