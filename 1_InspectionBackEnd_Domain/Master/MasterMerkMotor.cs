using _1_InspectionBackEnd_Domain.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_MERK_MOTOR")]
    public class MasterMerkMotor
    {
        [Key]
        public long? MERK_MOTOR_ID { get; private set; }
        [StringLength(255)]
        public string? NAMA_MERK_MOTOR { get; private set; }
        [StringLength(255)]
        public string? MERK_MOTOR_LOGO { get; private set; }
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }

        public static MasterMerkMotor CreateMerkMotor(MasterMerkMotorEntity masterMerkMotorEntity)
        {
            return new MasterMerkMotor
            {
                NAMA_MERK_MOTOR = masterMerkMotorEntity.NamaMerkMotor,
                MERK_MOTOR_LOGO = masterMerkMotorEntity.MerkMotorLogo,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = masterMerkMotorEntity.UserEmail
            };
        }
    }
    
    public class MasterMerkMotorEntity
    {
        public string? NamaMerkMotor { get; set; }
        public string? MerkMotorLogo
        {
            get
            {
                if (NamaMerkMotor != null)
                {
                    return "brand-logo/" + NamaMerkMotor.ToLower() + ".svg";
                }
                else
                {
                    return null;
                }

            }
        }
        public string? UserEmail { get; set; }
    }
}
