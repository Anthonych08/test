using _1_InspectionBackEnd_Domain.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_MODEL_MOTOR")]
    public class MasterModelMotor
    {
        [Key]
        public long? MODEL_MOTOR_ID { get; private set; }
        public long? MERK_MOTOR_ID { get; private set; }
        [StringLength(255)]
        public string? NAMA_MODEL_MOTOR { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static MasterModelMotor CreateModelMotor(MasterModelMotorEntity masterModelMotorEntity)
        {
            return new MasterModelMotor
            {
                MERK_MOTOR_ID = masterModelMotorEntity.MerkMotorId,
                NAMA_MODEL_MOTOR = masterModelMotorEntity.NamaModelMotor,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = masterModelMotorEntity.UserEmail
            };
        }
    }
    public class MasterModelMotorEntity
    {
        public long? MerkMotorId { get; set; }
        public string? NamaModelMotor { get; set; }
        public string? UserEmail { get; set; }
    }
}
