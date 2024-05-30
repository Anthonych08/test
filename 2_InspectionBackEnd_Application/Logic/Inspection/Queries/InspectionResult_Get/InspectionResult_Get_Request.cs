using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get
{
    public class InspectionResult_Get_Request
    {
        public long? TypeMotorId { get; set; }
        public long? TahunMotor {  get; set; }
    }
}
