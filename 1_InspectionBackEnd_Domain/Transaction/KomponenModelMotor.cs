using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Transaction
{
    [Table("KOMPONEN_MODEL_MOTOR")]
    public class KomponenModelMotor
    {
        [Key]
        public long? KOMPONEN_MODEL_MOTOR_ID { get; private set; }
        public long? KOMPONEN_MOTOR_ID { get; private set; }
        public long? MODEL_MOTOR_ID { get; private set; }
        public int? HARGA_KOMPONEN { get; private set; }
        [StringLength(255)]
        public string? SEARCH_KEYWORD { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static KomponenModelMotor CreateKomponenMotor(KomponenModelMotorEntity komponenModelMotorEntity)
        {
            return new KomponenModelMotor
            {
                KOMPONEN_MOTOR_ID = komponenModelMotorEntity.KomponenMotorId,
                MODEL_MOTOR_ID = komponenModelMotorEntity.ModelMotorId,
                HARGA_KOMPONEN = komponenModelMotorEntity.HargaKomponen,
                SEARCH_KEYWORD = komponenModelMotorEntity.SearchKeyword,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = komponenModelMotorEntity.UserEmail
            };
        }

        public void UpdateHargaKomponenModel(KomponenModelMotorEntity komponenModelMotorEntity)
        {
            HARGA_KOMPONEN = komponenModelMotorEntity.HargaKomponen;
            UPDATED_ON = DateTime.UtcNow;
            UPDATED_BY = komponenModelMotorEntity.UserEmail;
        }
    }
    public class KomponenModelMotorEntity
    {
        public long? KomponenMotorId { get; set; }
        public long? ModelMotorId { get; set; }
        public int? HargaKomponen { get; set; }
        public string? SearchKeyword { get; set; }
        public string? UserEmail { get; set; }
    }
}
