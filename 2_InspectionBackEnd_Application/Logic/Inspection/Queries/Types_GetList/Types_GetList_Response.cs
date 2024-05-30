using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Types_GetList
{
    public class Types_GetList_Response
    {
        public List<Types>? TypesList { get; set; }
    }
    public class Types
    {
        public long? TipeMotorId { get; set; }
        public string? TipeMotorName { get; set; }
        public string? TipeMotorPicture { get; set; }
        public List<int?>? TahunTipeMotor { get; set; }
    }
}
