using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_USERS")]
    public class MasterUser
    {
        [Key]
        public long USER_ID { get; private set; }
        [StringLength(255)]
        public string? EMAIL { get; private set; }
        [StringLength(255)]
        public string? PASSWORD { get; private set; }
        public long? ROLE_ID { get; private set; }
        public string? PROFILE_PICTURE { get; private set; }
        [StringLength(255)]
        public string? LOGIN_TYPE { get; private set; }
        public bool? IS_ACTIVE { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }

        public static MasterUser CreateMasterUser(UserEntity user)
        {
            return new MasterUser
            {
                EMAIL = user.Email,
                PASSWORD = user.Password,
                ROLE_ID = user.RoleId,
                PROFILE_PICTURE = user.ProfilePicture,
                LOGIN_TYPE = user.LoginType,
                IS_ACTIVE = true,
                CREATED_BY = user.Writer,
                CREATED_ON = DateTime.UtcNow,
            };
        }
    }
    public class UserEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public long? RoleId { get; set; }
        public string? ProfilePicture { get; set; }
        public string? LoginType {  get; set; }
        public string? Writer { get; set; }
        public bool? IsActive {  get; set; }
    }
}
