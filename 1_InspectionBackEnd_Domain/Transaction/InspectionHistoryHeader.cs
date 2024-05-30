using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_InspectionBackEnd_Domain.Transaction
{
    [Table("INSPECTION_HISTORY_HEADER")]
    public class InspectionHistoryHeader
    {
        [Key]
        public long? INSPECTION_HISTORY_HEADER_ID { get; private set; }
        public long? MASTER_USER_ID { get; private set; }
        public long? TIPE_MOTOR_ID { get; private set; }
        public int? TAHUN_MOTOR { get; private set; }
        public int? HARGA_MOTOR_OLX { get; private set; }
        public int? HARGA_PERBAIKAN { get; private set; }
        public bool? IS_DELETED { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }

        public static InspectionHistoryHeader CreateInspectionHistoryHeader(InspectionHistoryHeaderEntity historyHeaderEntity)
        {
            return new InspectionHistoryHeader
            {
                MASTER_USER_ID = historyHeaderEntity.MasterUserId,
                TIPE_MOTOR_ID = historyHeaderEntity.TipeMotorId,
                TAHUN_MOTOR = historyHeaderEntity.TahunMotor,
                HARGA_MOTOR_OLX = historyHeaderEntity.HargaMotorOlx,
                HARGA_PERBAIKAN = historyHeaderEntity.HargaPerbaikan,
                IS_DELETED = historyHeaderEntity.IsDeleted,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = historyHeaderEntity.WriterEmail
            };
        }
        public void SoftDelete()
        {
            IS_DELETED = true;
        }
        public void RefreshPrices(InspectionHistoryHeaderEntity newPrices)
        {
            HARGA_MOTOR_OLX = newPrices.HargaMotorOlx;
            HARGA_PERBAIKAN = newPrices.HargaPerbaikan;
            UPDATED_BY = newPrices.WriterEmail;
            UPDATED_ON = DateTime.UtcNow;
        }
    }
    public class InspectionHistoryHeaderEntity
    {
        public long? MasterUserId { get; set; }
        public long? TipeMotorId { get; set; }
        public int? TahunMotor { get; set; }
        public int? HargaMotorOlx { get; set; }
        public int? HargaPerbaikan { get; set; }
        public bool? IsDeleted {  get; set; }
        public string? WriterEmail { get; set; }

    }
}
