using _1_InspectionBackEnd_Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Interfaces
{
    public interface IJwt
    {
        public string GenerateToken(MasterUser user, string roleName);
    }
}
