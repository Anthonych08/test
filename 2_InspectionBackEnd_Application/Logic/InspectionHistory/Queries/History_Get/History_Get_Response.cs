using _1_InspectionBackEnd_Domain.Transaction;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_Get
{
    public class History_Get_Response
    {
        public int? HargaMotorOlx {  get; set; }
        public int? HargaPerbaikan { get; set; }
        public List<HeaderComponent>? HeaderComponents { get; set; }
    };
    public class HeaderComponent
    {
        public string? HeaderComponentName { get; set; }
        public List<DetailComponent>? DetailComponents { get; set; }
    }
    public class DetailComponent
    {
        public long? KomponenMotorId { get; set; }
        public long? KomponenModelMotorId { get; set; }
        public string? NamaKomponenMotor { get; set; }
        public int? HargaKomponenMotor { get; set; }
        public bool? NeedReplacement { get; set; }
    }
}
