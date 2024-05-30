using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Transaction
{
    [Table("ROLE_MENU")]
    public class RoleMenu
    {
        [Key]
        public long? ROLE_MENU_ID { get; set; }
        public long? ROLE_ID { get; set; }
        public long? MENU_ID { get; set; }
        public bool? IS_ACTIVE { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }
        public static RoleMenu CreateRoleMenu(RoleMenuEntity roleMenu)
        {
            return new RoleMenu
            {
                ROLE_ID = roleMenu.RoleId,
                MENU_ID = roleMenu.MenuId,
                IS_ACTIVE = true,
                CREATED_ON = DateTime.UtcNow,
                CREATED_BY = roleMenu.Writer,
            };
        }
    }
    public class RoleMenuEntity
    {
        public long? RoleId { get; set; }
        public long? MenuId { get; set; }
        public string? Writer { get; set; }
        public bool? IsActive { get; set; }
    }
}
