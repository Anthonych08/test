using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Brands_GetList
{
    public class Brands_GetList_Response
    {
        public List<Brands>? BrandsList { get; set; }
    }
    public class Brands
    {
        public long? MerkMotorId { get; set; }
        public string? NamaMerkMotor { get; set; }
        public string? MerkMotorLogo { get; set; }
    }
}
