using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Models_GetList
{
    public class Models_GetList_Response
    {
        public List<Models>? ModelsList { get; set; }
    }
    public class Models
    {
        public long? ModelMotorId { get; set; }
        public string? NamaModelMotor { get; set; }
    }
}
