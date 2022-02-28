using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HrApp.Services
{
    public class IdentityRepositoryResult
    {
        public IEnumerable<string> Errors => errors; public IdentityUser IdentityUser { get; set; }
        public bool Succeeded { get; set; }
        public void AddError(string error) { errors.Add(error); }
        List<string> errors = new List<string>();
    }
}
