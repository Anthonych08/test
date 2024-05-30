using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_InspectionBackEnd_Shared.Constants
{
    public enum Error_Type
    {
        [Description("UNKNOWN")] Unknown,
        [Description("VALIDATION")] Validation,
        [Description("BAD_REQUEST")] BadRequest,
        [Description("NOT_FOUND")] NotFound,
        [Description("UNAUTHORIZE_ACCESS")] UnauthorizedAccess,
        [Description("UNAUTHENTICATED")] Unauthenticated,
        [Description("DOMAIN_EXCEPTION")] DomainException,
    }
}
