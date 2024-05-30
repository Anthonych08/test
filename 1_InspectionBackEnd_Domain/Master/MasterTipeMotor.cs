using _1_InspectionBackEnd_Domain.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_TIPE_MOTOR")]
    public class MasterTipeMotor
    {
        [Key]
        public long? TIPE_MOTOR_ID { get; private set; }
        public long? MODEL_MOTOR_ID { get; private set; }
        [StringLength(255)]
        public string? NAMA_TIPE_MOTOR { get; private set; }
        public int? START_TAHUN_TIPE_MOTOR { get; private set; }
        public int? END_TAHUN_TIPE_MOTOR { get; private set; }
        [StringLength(255)]
        public string? GAMBAR_TIPE_MOTOR { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static MasterTipeMotor CreateTipeMotor(MasterTipeMotorEntity masterTipeMotorEntity)
        {
            return new MasterTipeMotor
            {
                MODEL_MOTOR_ID = masterTipeMotorEntity.ModelMotorId,
                NAMA_TIPE_MOTOR = masterTipeMotorEntity.NamaTipeMotor,
                START_TAHUN_TIPE_MOTOR = masterTipeMotorEntity.StartTahunMotor,
                END_TAHUN_TIPE_MOTOR = masterTipeMotorEntity.EndTahunMotor,
                GAMBAR_TIPE_MOTOR = masterTipeMotorEntity.GambarTipeMotor,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = masterTipeMotorEntity.UserEmail
            };
        }
    }
    public class MasterTipeMotorEntity
    {
        public long? ModelMotorId { get; set; }
        public string? NamaTipeMotor { get; set; }
        public string? NamaMerkMotor { get; set; }
        public int? StartTahunMotor { get; set; }
        public int? EndTahunMotor { get; set; }
        public string? GambarTipeMotor { get
            {
                if (NamaTipeMotor != null)
                {
                    TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                    return "tipe-motor/"+ NamaMerkMotor +"/" + info.ToTitleCase(NamaTipeMotor).Replace(" ", string.Empty) + ".png";
                }
                else
                {
                    return null;
                };
            }
        }
        public int? HargaMotorOlx { get; set; } = 0;
        public string? UserEmail { get; set; }
    }
}
