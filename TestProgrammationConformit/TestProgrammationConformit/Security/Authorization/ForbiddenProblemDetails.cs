using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TestProgrammationConformit.Authorization
{
    public class ForbiddenProblemDetails:ProblemDetails
    {
        public ForbiddenProblemDetails(string details = null)
        {
            Title = "Forbidden";
            Detail = details;
            Status = 403;
            Type = "https://httpstatuses.com/403";
        }
    }
}
