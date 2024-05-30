using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_InspectionBackEnd_Domain.Transaction
{
    [Table("INSPECTION_HISTORY_DETAIL")]
    public class InspectionHistoryDetail
    {
        [Key]
        public long? INSPECTION_HISTORY_DETAIL_ID { get; private set; }
        public long? INSPECTION_HISTORY_HEADER_ID { get; private set; }
        public long? MASTER_KOMPONEN_ID { get; private set; }
        public int? HARGA_KOMPONEN_TOKOPEDIA { get; private set; }
        public bool? STATUS_PERBAIKAN { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }

        public static InspectionHistoryDetail CreateInspectionHistoryDetail(InspectionHistoryDetailEntity historyDetailEntity)
        {
            return new InspectionHistoryDetail
            {
                INSPECTION_HISTORY_HEADER_ID = historyDetailEntity.InspectionHistoryHeaderId,
                MASTER_KOMPONEN_ID = historyDetailEntity.MasterKomponenId,
                HARGA_KOMPONEN_TOKOPEDIA = historyDetailEntity.HargaKomponenTokopedia,
                STATUS_PERBAIKAN = historyDetailEntity.StatusPerbaikan,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = historyDetailEntity.WriterEmail
            };
        }
        public void RefreshPrices (InspectionHistoryDetailEntity newDetail)
        {
            HARGA_KOMPONEN_TOKOPEDIA = newDetail.HargaKomponenTokopedia;
            UPDATED_BY = newDetail.WriterEmail;
            UPDATED_ON = DateTime.UtcNow;
        }
    }
    public class InspectionHistoryDetailEntity
    {
        public long? InspectionHistoryHeaderId { get; set; }
        public long? MasterKomponenId { get; set; }
        public int? HargaKomponenTokopedia { get; set; }
        public bool? StatusPerbaikan { get; set; }
        public string? WriterEmail { get; set; }
    }
}
