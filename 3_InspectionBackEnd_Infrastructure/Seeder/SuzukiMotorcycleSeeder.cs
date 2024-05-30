using _0_InspectionBackEnd_Shared.GeneralFunctions;
using _1_InspectionBackEnd_Domain.Master;
using _1_InspectionBackEnd_Domain.Transaction;
using _3_InspectionBackEnd_Infrastructure.DataSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.Seeder
{
    public static class SuzukiMotorcycleSeeder
    {
        public static void SuzukiMotorcycleSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddSuzuki(context);
            Console.Clear();
        }
        private static void AddSuzuki(InspectionDatabaseContext context)
        {
            //Check jika ada Merk Suzuki di DB
            var checkSuzukiExist = context.MasterMerkMotors.Where(w => w.NAMA_MERK_MOTOR == "Suzuki").FirstOrDefault();
            if (checkSuzukiExist == null)
            {
                //Add Ketika tidak ada
                var newlyCreatedModel = MasterMerkMotor.CreateMerkMotor(new MasterMerkMotorEntity
                {
                    NamaMerkMotor = "Suzuki",
                    UserEmail = "SEEDER"
                });
                context.MasterMerkMotors.Add(newlyCreatedModel);
                context.SaveChanges();

                checkSuzukiExist = newlyCreatedModel;
            }

            //Tambahkan model untuk merk Suzuki
            AddSuzukiModels(context, checkSuzukiExist.MERK_MOTOR_ID, checkSuzukiExist.NAMA_MERK_MOTOR);

            context.SaveChanges();
        }
        private static void AddSuzukiModels(InspectionDatabaseContext context, long? MerkMotorId, string? namaMerkMotor)
        {
            //Tambahkan model GSX jika tidak ada di database
            var checkGSXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "GSX").FirstOrDefault();
            if (checkGSXModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "GSX",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Satria jika tidak ada di database
            var checkSatriaModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Satria").FirstOrDefault();
            if (checkSatriaModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Satria",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkkan model motor lain jika tidak ada
            //
            context.SaveChanges();

            //Tambahkan tipe motor tergantung merk dan modelnya
            AddSuzukiType(context, namaMerkMotor);

            //Tambahkan tahun tipe motor tergantung dengan tipe dan tahunnya
            AddSuzukiTahunType(context, MerkMotorId);

            //Tambahkan Component pada tiap model motor
            AddSuzukiComponent(context, MerkMotorId);
        }

        private static void AddSuzukiType(InspectionDatabaseContext context, string? namaMerkMotor)
        {
            //Add GSX type & year
            var checkGSXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "GSX").FirstOrDefault();
            if (checkGSXModelExist != null)
            {
                var currTipeGSX = new List<MasterTipeMotor>();

                var allTipeListDbGSX = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkGSXModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe GSX 150 from 2016 - 2024
                currTipeGSX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkGSXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "GSX 150",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeGSX.Where(w => !allTipeListDbGSX.Select(s => new
                    {
                        ModelMotorId = s.MODEL_MOTOR_ID,
                        NamaTipe = s.NAMA_TIPE_MOTOR,
                    }).Contains(new
                    {
                        ModelMotorId = w.MODEL_MOTOR_ID,
                        NamaTipe = w.NAMA_TIPE_MOTOR,
                    })
                    ).ToList());

                context.SaveChanges();
            };

            //Add Satria type & year
            var checkSatriaModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Satria").FirstOrDefault();
            if (checkSatriaModelExist != null)
            {
                var currTipeSatria = new List<MasterTipeMotor>();

                var allTipeListDbSatria = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkSatriaModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Satria F 150 from 2010 - 2016
                    currTipeSatria.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSatriaModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Satria F 150",
                        StartTahunMotor = 2010,
                        EndTahunMotor = 2016,
                        UserEmail = "SEEDER"
                    }));

                //tipe Satria FU 150 from 2016 - 2024
                    currTipeSatria.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSatriaModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Satria FU 150",
                        StartTahunMotor = 2016,
                        EndTahunMotor = 2024,
                        UserEmail = "SEEDER"
                    }));

                context.MasterTipeMotors.AddRange(
                    currTipeSatria.Where(w => !allTipeListDbSatria.Select(s => new
                    {
                        ModelMotorId = s.MODEL_MOTOR_ID,
                        NamaTipe = s.NAMA_TIPE_MOTOR,
                    }).Contains(new
                    {
                        ModelMotorId = w.MODEL_MOTOR_ID,
                        NamaTipe = w.NAMA_TIPE_MOTOR,
                    })
                    ).ToList());

                context.SaveChanges();
            };

            context.SaveChanges();
        }

        private static void AddSuzukiComponent(InspectionDatabaseContext context, long? MerkMotorId)
        {
            //Tambahkan komponen untuk model Vario 150 Tahun 2015
            var allModelMotor = context.MasterModelMotors
                .Where(w => w.MERK_MOTOR_ID == MerkMotorId)
                .ToList();

            var allDbComponentParts = context.MasterKomponenMotors.Where(w => w.TIPE_KOMPONEN_MOTOR != "Papers").ToList();

            var allExistedDbComponentMotor = context.KomponenModelMotors
                .Where(w => allModelMotor.Select(s => s.MODEL_MOTOR_ID).Contains(w.MODEL_MOTOR_ID))
                .ToList();

            var currComponentMotorList = new List<KomponenModelMotor>();

            if (allModelMotor.Count > 0)
            {
                foreach (var modelMotor in allModelMotor)
                {
                    foreach (var componentParts in allDbComponentParts)
                    {
                        currComponentMotorList.Add(KomponenModelMotor.CreateKomponenMotor(new KomponenModelMotorEntity
                        {
                            KomponenMotorId = componentParts.KOMPONEN_MOTOR_ID,
                            ModelMotorId = modelMotor.MODEL_MOTOR_ID,
                            HargaKomponen = 0,
                            SearchKeyword = componentParts.SEARCH_KEYWORD + " " + modelMotor.NAMA_MODEL_MOTOR,
                            UserEmail = "Seeder"
                        }));
                    }
                }

                context.KomponenModelMotors.AddRange(
                    currComponentMotorList.Where(w => !allExistedDbComponentMotor.Select(s => new
                    {
                        KomponenMotorId = s.KOMPONEN_MOTOR_ID,
                        ModelMotorId = s.MODEL_MOTOR_ID,
                    }).Contains(new
                    {
                        KomponenMotorId = w.KOMPONEN_MOTOR_ID,
                        ModelMotorId = w.MODEL_MOTOR_ID,
                    })
                    ).ToList());
            };
            context.SaveChanges();
        }
        private static void AddSuzukiTahunType(InspectionDatabaseContext context, long? MerkMotorId)
        {
            //Tambahkan komponen
            var allModelMotor = context.MasterModelMotors
                .Where(w => w.MERK_MOTOR_ID == MerkMotorId)
                .ToList();

            var allTypeMotor = context.MasterTipeMotors
                .Where(w => allModelMotor.Select(s => s.MODEL_MOTOR_ID).Contains(w.MODEL_MOTOR_ID))
                .ToList();

            var dbTahunTipeMotorList = (from a in context.TahunTipeMotors

                                        join b in context.MasterTipeMotors on new { a.TIPE_MOTOR_ID } equals new { b.TIPE_MOTOR_ID } into abGroup
                                        from b in abGroup.DefaultIfEmpty()

                                        where allTypeMotor.Select(s => s.TIPE_MOTOR_ID).Contains(a.TIPE_MOTOR_ID)

                                        select new TahunTipeMotorEntity
                                        {
                                            NamaTipeMotor = b.NAMA_TIPE_MOTOR,
                                            TahunMotor = a.TAHUN_TIPE_MOTOR,
                                            HargaMotorOlx = a.HARGA_MOTOR_OLX,
                                            TipeMotorId = a.TIPE_MOTOR_ID
                                        }).ToList();

            var currTahunTipeMotorList = new List<TahunTipeMotor>();

            if (allTypeMotor.Count > 0)
            {
                foreach (var typeMotor in allTypeMotor)
                {
                    for (var i = typeMotor.START_TAHUN_TIPE_MOTOR; i <= typeMotor.END_TAHUN_TIPE_MOTOR; i++)
                    {
                        currTahunTipeMotorList.Add(TahunTipeMotor.CreateTipeTahunMotor(new TahunTipeMotorEntity
                        {
                            TipeMotorId = typeMotor.TIPE_MOTOR_ID,
                            TahunMotor = i,
                            NamaTipeMotor = typeMotor.NAMA_TIPE_MOTOR,
                            HargaMotorOlx = 0,
                            UserEmail = "SEEDER"
                        }));
                    }
                }

                context.TahunTipeMotors.AddRange(
                    currTahunTipeMotorList.Where(w => !dbTahunTipeMotorList.Select(s => new
                    {
                        TipeMotorId = s.TipeMotorId,
                        TahunMotor = s.TahunMotor,
                    }).Contains(new
                    {
                        TipeMotorId = w.TIPE_MOTOR_ID,
                        TahunMotor = w.TAHUN_TIPE_MOTOR,
                    })
                    ).ToList());
            };
            context.SaveChanges();
        }
    }
}
