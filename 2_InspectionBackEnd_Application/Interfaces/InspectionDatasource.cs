using _1_InspectionBackEnd_Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _1_InspectionBackEnd_Domain.Transaction;

namespace _2_InspectionBackEnd_Application.Interfaces
{
    public interface IInspection_Datasource : IDatabaseContext
    {
        public DbSet<MasterUser> MasterUsers { get; set; }
        public DbSet<MasterMenu> MasterMenus { get; set; }
        public DbSet<MasterRole> MasterRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<MasterMerkMotor> MasterMerkMotors { get; set; }
        public DbSet<MasterModelMotor> MasterModelMotors { get; set; }
        public DbSet<MasterTipeMotor> MasterTipeMotors { get; set; }
        public DbSet<TahunTipeMotor> TahunTipeMotors { get; set; }
        public DbSet<MasterKomponenMotor> MasterKomponenMotors { get; set; }
        public DbSet<KomponenModelMotor> KomponenModelMotors { get; set; }
        public DbSet<InspectionHistoryHeader> InspectionHistoryHeaders { get; set; }
        public DbSet<InspectionHistoryDetail> InspectionHistoryDetails { get; set; }
    }
}
