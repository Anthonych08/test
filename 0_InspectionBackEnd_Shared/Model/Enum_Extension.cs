using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_InspectionBackEnd_Shared.Extensions
{
    public class HistoryModel
    {
        public long? Id { get; set; }
        public string? User {  get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? Image {  get; set; }

    }
}
