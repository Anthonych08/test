using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Commands.SaveInspection
{
    public class SaveInspection_Request
    {
        public string? Email { get; set; }
        public long? TipeMotorId { get; set; }
        public int? TahunMotor { get; set; }
        public int? HargaPerbaikan { get; set; }
        public int? HargaMotorOlx { get; set; }
        public List<DetailComponent>? DetailComponents { get; set; }
    }
    public class DetailComponent
    {
        public long? KomponenMotorId { get; set; }
        public string? NamaKomponenMotor { get; set; }
        public int? HargaKomponenMotor { get; set; }
        public bool? NeedReplacement { get; set; } = false;
    }
}
