using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_ROLES")]
    public class MasterRole
    {
        [Key]
        public long ROLE_ID { get; set; }
        [StringLength(255)]
        public string? ROLE_NAME { get; set; }
        public bool? IS_ACTIVE { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static MasterRole CreateMasterRole(RoleEntity role)
        {
            return new MasterRole
            {
                ROLE_NAME = role.RoleName,
                IS_ACTIVE = true,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = role.Writer,
            };
        }
    }
    public class RoleEntity
    {
        public string? RoleName { get; set; }
        public string? Writer { get; set; }
        public string? IsActive {  get; set; }
    }
}
