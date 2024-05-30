using _1_InspectionBackEnd_Domain.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_KOMPONEN_MOTOR")]
    public class MasterKomponenMotor
    {
        [Key]
        public long? KOMPONEN_MOTOR_ID { get; private set; }
        [StringLength(255)]
        public string? TIPE_KOMPONEN_MOTOR { get; private set; }
        [StringLength(255)]
        public string? NAMA_KOMPONEN_MOTOR { get; private set; }
        [StringLength(255)]
        public string? DESKRIPSI_KOMPONEN { get; private set; }
        [StringLength(255)]
        public string? SEARCH_KEYWORD { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static MasterKomponenMotor CreateKomponenMotor(MasterKomponenMotorEntity masterKomponenMotorEntity)
        {
            return new MasterKomponenMotor
            {
                TIPE_KOMPONEN_MOTOR = masterKomponenMotorEntity.TipeKomponenMotor,
                NAMA_KOMPONEN_MOTOR = masterKomponenMotorEntity.NamaKomponenMotor,
                DESKRIPSI_KOMPONEN = masterKomponenMotorEntity.DeskripsiKomponen,
                SEARCH_KEYWORD = masterKomponenMotorEntity.SearchKeyword,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = masterKomponenMotorEntity.UserEmail
            };
        }
    }
    public class MasterKomponenMotorEntity
    {
        public string? TipeKomponenMotor { get; set; }
        public string? NamaKomponenMotor { get; set; }
        public string? DeskripsiKomponen { get; set; }
        public string? SearchKeyword {  get; set; }
        public string? UserEmail { get; set; }
    }
}
