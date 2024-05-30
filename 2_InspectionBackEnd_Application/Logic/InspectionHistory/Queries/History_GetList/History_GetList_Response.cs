using _1_InspectionBackEnd_Domain.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_GetList
{
    public class History_GetList_Response
    {
        public List<HistoryCardHeader>? History { get; set; }
    };
    public class HistoryCardHeader
    {
        public long? InspectionHistoryHeaderId { get; set; }
        public string? MerkLogoUrl { get; set; }
        public string? TipeMotorName { get; set; }
        public int? Year { get; set; }
        public string? TipeMotorPicUrl { get; set; }
        public int? AfterRepairPrice {  get; set; }
    };
}
