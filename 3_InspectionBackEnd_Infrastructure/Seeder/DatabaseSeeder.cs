using _0_InspectionBackEnd_Shared.GeneralFunctions;
using _1_InspectionBackEnd_Domain.Master;
using _1_InspectionBackEnd_Domain.Transaction;
using _3_InspectionBackEnd_Infrastructure.DataSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.Seeder
{
    public static class DatabaseSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddRole(context);
            AddUser(context);
            AddMenu(context);
            AddRoleMenu(context);
            AddMasterKomponen(context);
            Console.Clear();
        }
        private static void AddRole(InspectionDatabaseContext context)
        {
            var role = context.MasterRoles.OrderBy(w => w.ROLE_ID).FirstOrDefault();
            if (role != null) { return; }
            context.MasterRoles.Add(MasterRole.CreateMasterRole(new RoleEntity
            {
                RoleName = "Admin",
                Writer = "SEEDER"
            }));
            context.MasterRoles.Add(MasterRole.CreateMasterRole(new RoleEntity
            {
                RoleName = "Employee",
                Writer = "SEEDER"
            }));
            context.SaveChanges();
        }
        private static void AddUser(InspectionDatabaseContext context)
        {
            var user = context.MasterUsers.OrderBy(w => w.USER_ID).FirstOrDefault();
            if (user != null) { return; }
            context.MasterUsers.Add(MasterUser.CreateMasterUser(new UserEntity
            {
                Email = "employee@gmail.com",
                Password = PasswordOperation.HashPassword("Testing123"),
                ProfilePicture = null,
                RoleId = 2,
                LoginType = "Database",
                Writer = "SEEDER"
            }));
            context.MasterUsers.Add(MasterUser.CreateMasterUser(new UserEntity
            {
                Email = "admin@gmail.com",
                Password = PasswordOperation.HashPassword("Testing123"),
                ProfilePicture = null,
                RoleId = 1,
                LoginType = "Database",
                Writer = "SEEDER"
            }));
            context.SaveChanges();
        }
        private static void AddMenu(InspectionDatabaseContext context)
        {
            var menu = context.MasterMenus.OrderBy(w => w.MENU_ID).FirstOrDefault();
            if (menu != null) { return; }
            context.MasterMenus.Add(MasterMenu.CreateMenu(new MenuEntity
            {
                MenuName = "Callender",
                Writer = "SEEDER"
            }));
            context.MasterMenus.Add(MasterMenu.CreateMenu(new MenuEntity
            {
                MenuName = "Office",
                Writer = "SEEDER"
            }));
            context.MasterMenus.Add(MasterMenu.CreateMenu(new MenuEntity
            {
                MenuName = "Room Meeting",
                Writer = "SEEDER"
            }));
            context.MasterMenus.Add(MasterMenu.CreateMenu(new MenuEntity
            {
                MenuName = "Furniture",
                Writer = "SEEDER"
            }));
            context.SaveChanges();
        }
        private static void AddRoleMenu(InspectionDatabaseContext context)
        {
            var roleMenu = context.RoleMenus.OrderBy(w => w.ROLE_MENU_ID).FirstOrDefault();
            if (roleMenu != null) { return; }
            context.RoleMenus.Add(RoleMenu.CreateRoleMenu(new RoleMenuEntity
            {
                RoleId = 2,
                MenuId = 1,
                Writer = "SEEDER"
            }));
            context.RoleMenus.Add(RoleMenu.CreateRoleMenu(new RoleMenuEntity
            {
                RoleId = 1,
                MenuId = 1,
                Writer = "SEEDER"
            }));
            context.RoleMenus.Add(RoleMenu.CreateRoleMenu(new RoleMenuEntity
            {
                RoleId = 1,
                MenuId = 2,
                Writer = "SEEDER"
            }));
            context.RoleMenus.Add(RoleMenu.CreateRoleMenu(new RoleMenuEntity
            {
                RoleId = 1,
                MenuId = 3,
                Writer = "SEEDER"
            }));
            context.RoleMenus.Add(RoleMenu.CreateRoleMenu(new RoleMenuEntity
            {
                RoleId = 1,
                MenuId = 4,
                Writer = "SEEDER"
            }));
            context.SaveChanges();
        }

        private static void AddMasterKomponen(InspectionDatabaseContext context)
        {
            var masterKomponen = context.MasterKomponenMotors.OrderBy(w => w.KOMPONEN_MOTOR_ID).FirstOrDefault();
            if (masterKomponen != null) { return; }

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Tire",
                NamaKomponenMotor = "Front",
                DeskripsiKomponen = "",
                SearchKeyword = "Ban Depan",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Tire",
                NamaKomponenMotor = "Rear",
                DeskripsiKomponen = "",
                SearchKeyword = "Ban Belakang",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Brake",
                NamaKomponenMotor = "Front",
                DeskripsiKomponen = "",
                SearchKeyword = "Kampas Rem Depan",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Brake",
                NamaKomponenMotor = "Rear",
                DeskripsiKomponen = "",
                SearchKeyword = "Kampas Rem Belakang",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Electrical",
                NamaKomponenMotor = "Headlight",
                DeskripsiKomponen = "",
                SearchKeyword = "Bohlam Lampu Depan",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Electrical",
                NamaKomponenMotor = "Tail light",
                DeskripsiKomponen = "",
                SearchKeyword = "Bohlam Lampu Belakang",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Electrical",
                NamaKomponenMotor = "Acu",
                DeskripsiKomponen = "",
                SearchKeyword = "Aki",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Electrical",
                NamaKomponenMotor = "Horn",
                DeskripsiKomponen = "",
                SearchKeyword = "Klakson",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Engine",
                NamaKomponenMotor = "Oil",
                DeskripsiKomponen = "",
                SearchKeyword = "Paket Oli",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Engine",
                NamaKomponenMotor = "Starter",
                DeskripsiKomponen = "",
                SearchKeyword = "Dinamo Stater",
                UserEmail = "Seeder"
            }));


            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Engine",
                NamaKomponenMotor = "Piston",
                DeskripsiKomponen = "",
                SearchKeyword = "Piston Kit",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Engine",
                NamaKomponenMotor = "Throttle Cable",
                DeskripsiKomponen = "",
                SearchKeyword = "Kabel Gas",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Suspension",
                NamaKomponenMotor = "Front Shock Breaker",
                DeskripsiKomponen = "",
                SearchKeyword = "Shock Depan",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Suspension",
                NamaKomponenMotor = "Rear Shock Breaker",
                DeskripsiKomponen = "",
                SearchKeyword = "Shock Belakang",
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Papers",
                NamaKomponenMotor = "BPKB",
                DeskripsiKomponen = "",
                SearchKeyword = null,
                UserEmail = "Seeder"
            }));

            context.MasterKomponenMotors.Add(MasterKomponenMotor.CreateKomponenMotor(new MasterKomponenMotorEntity
            {
                TipeKomponenMotor = "Papers",
                NamaKomponenMotor = "STNK",
                DeskripsiKomponen = "",
                SearchKeyword = null,
                UserEmail = "Seeder"
            }));

            context.SaveChanges();
        }
    }
}
