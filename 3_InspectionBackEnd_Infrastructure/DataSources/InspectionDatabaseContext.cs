using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using _1_InspectionBackEnd_Domain.Master;
using _2_InspectionBackEnd_Application.Interfaces;
using Microsoft.Extensions.Options;
using _3_InspectionBackEnd_Infrastructure.Settings;
using _1_InspectionBackEnd_Domain.Transaction;

namespace _3_InspectionBackEnd_Infrastructure.DataSources
{
    public class InspectionDatabaseContext : DbContext, IInspection_Datasource
    {
        public DbContext Instance => this;
        public InspectionDatabaseContext (DbContextOptions<InspectionDatabaseContext> options
            ) : base(options)
        {

        }
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
