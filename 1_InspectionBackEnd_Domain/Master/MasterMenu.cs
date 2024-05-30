using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_InspectionBackEnd_Domain.Master
{
    [Table("MASTER_MENU")]
    public class MasterMenu
    {
        [Key]
        public long MENU_ID { get; private set; }
        [StringLength(255)]
        public string? MENU_NAME { get; private set; }
        public bool? IS_ACTIVE { get; private set; }
        [StringLength(255)]
        public DateTime? CREATED_ON { get; private set; }
        [StringLength(255)]
        public string? CREATED_BY { get; private set; }
        public DateTime? UPDATED_ON { get; private set; }
        [StringLength(255)]
        public string? UPDATED_BY { get; private set; }

        public static MasterMenu CreateMenu(MenuEntity menuEntity)
        {
            return new MasterMenu
            {
                MENU_NAME = menuEntity.MenuName,
                IS_ACTIVE = true,
                CREATED_BY = menuEntity.Writer,
                CREATED_ON = DateTime.UtcNow,
            };
        }
    }
    public class MenuEntity
    {
        public string? MenuName { get; set; }
        public string? Writer { get; set; }
        public bool? IsActive { get; set; }
    }
}
