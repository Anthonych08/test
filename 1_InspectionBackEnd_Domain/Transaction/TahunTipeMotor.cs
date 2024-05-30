using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace _1_InspectionBackEnd_Domain.Transaction
{
    [Table("TAHUN_TIPE_MOTOR")]
    public class TahunTipeMotor
    {
        [Key]
        public long? TAHUN_TIPE_MOTOR_ID { get; private set; }
        public long? TIPE_MOTOR_ID { get; private set; }
        public int? TAHUN_TIPE_MOTOR { get; private set; }
        [StringLength(255)]
        public string? SEARCH_KEYWORD { get; private set; }
        public int? HARGA_MOTOR_OLX { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static TahunTipeMotor CreateTipeTahunMotor(TahunTipeMotorEntity tahunTipeMotorEntity)
        {
            return new TahunTipeMotor
            {
                TIPE_MOTOR_ID = tahunTipeMotorEntity.TipeMotorId,
                TAHUN_TIPE_MOTOR = tahunTipeMotorEntity.TahunMotor,
                SEARCH_KEYWORD = tahunTipeMotorEntity.SearchKeyword,
                HARGA_MOTOR_OLX = tahunTipeMotorEntity.HargaMotorOlx,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = tahunTipeMotorEntity.UserEmail
            };
        }
        public void UpdateHargaTipeTahunMotor(TahunTipeMotorEntity tahunTipeMotorEntity)
        {
            HARGA_MOTOR_OLX = tahunTipeMotorEntity.HargaMotorOlx;
            UPDATED_ON = DateTime.UtcNow;
            UPDATED_BY = tahunTipeMotorEntity.UserEmail;
        }
    }
    public class TahunTipeMotorEntity
    {
        public long? TipeMotorId { get; set; }
        public string? NamaTipeMotor { get; set; }
        public int? TahunMotor { get; set; }
        public string? SearchKeyword
        {
            get
            {
                return NamaTipeMotor + " " + TahunMotor.ToString();
            }
        }
        public int? HargaMotorOlx { get; set; } = 0;
        public string? UserEmail { get; set; }
    }
}
