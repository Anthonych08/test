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
    public static class KawasakiMotorcycleSeeder
    {
        public static void KawasakiMotorcycleSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddKawasaki(context);
            Console.Clear();
        }
        private static void AddKawasaki(InspectionDatabaseContext context)
        {
            //Check jika ada Merk Kawasaki di DB
            var checkKawasakiExist = context.MasterMerkMotors.Where(w => w.NAMA_MERK_MOTOR == "Kawasaki").FirstOrDefault();
            if (checkKawasakiExist == null)
            {
                //Add Ketika tidak ada
                var newlyCreatedModel = MasterMerkMotor.CreateMerkMotor(new MasterMerkMotorEntity
                {
                    NamaMerkMotor = "Kawasaki",
                    UserEmail = "SEEDER"
                });
                context.MasterMerkMotors.Add(newlyCreatedModel);
                context.SaveChanges();

                checkKawasakiExist = newlyCreatedModel;
            }

            //Tambahkan model untuk merk kawasaki
            AddKawasakiModels(context, checkKawasakiExist.MERK_MOTOR_ID, checkKawasakiExist.NAMA_MERK_MOTOR);

            context.SaveChanges();
        }
        private static void AddKawasakiModels(InspectionDatabaseContext context, long? MerkMotorId, string? namaMerkMotor)
        {
            //Tambahkan model Ninja 250 jika tidak ada di database
            var checkNinja250ModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Ninja 250").FirstOrDefault();
            if (checkNinja250ModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Ninja 250",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model KLX jika tidak ada di database
            var checkKLXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "KLX").FirstOrDefault();
            if (checkKLXModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "KLX",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model W175 jika tidak ada di database
            var checkW175ModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "W175").FirstOrDefault();
            if (checkW175ModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "W175",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkkan model motor lain jika tidak ada
            //
            context.SaveChanges();

            //Tambahkan tipe motor tergantung merk dan modelnya
            AddKawasakiType(context, namaMerkMotor);

            //Tambahkan tahun tipe motor tergantung dengan tipe dan tahunnya
            AddKawasakiTahunType(context, MerkMotorId);

            //Tambahkan Component pada tiap model motor
            AddKawasakiComponent(context, MerkMotorId);

        }

        private static void AddKawasakiType(InspectionDatabaseContext context, string? namaMerkMotor)
        {
            //Add Ninja 250 type & year
            var checkNinja250ModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Ninja 250").FirstOrDefault();
            if (checkNinja250ModelExist != null)
            {
                var currTipeNinja250 = new List<MasterTipeMotor>();

                var allTipeListDbNinja250 = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkNinja250ModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Ninja 250 from 2008 - 2012
                currTipeNinja250.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkNinja250ModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Ninja 250",
                    StartTahunMotor = 2008,
                    EndTahunMotor = 2012,
                    UserEmail = "SEEDER"
                }));

                //tipe Ninja 250 FI from 2013 - 2017
                currTipeNinja250.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkNinja250ModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Ninja 250 FI",
                    StartTahunMotor = 2013,
                    EndTahunMotor = 2017,
                    UserEmail = "SEEDER"
                }));

                //tipe Ninja 250 SL from 2014 - 2017
                currTipeNinja250.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkNinja250ModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Ninja 250 SL",
                    StartTahunMotor = 2014,
                    EndTahunMotor = 2017,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeNinja250.Where(w => !allTipeListDbNinja250.Select(s => new
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

            //Add KLX type & year
            var checkKLXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "KLX").FirstOrDefault();
            if (checkKLXModelExist != null)
            {
                var currTipeKLX = new List<MasterTipeMotor>();

                var allTipeListDbKLX = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkKLXModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe KLX 125 from 2010 - 2024
                currTipeKLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkKLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "KLX 125",
                    StartTahunMotor = 2010,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                //tipe KLX 150 from 2009 - 2024
                currTipeKLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkKLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "KLX 150",
                    StartTahunMotor = 2013,
                    EndTahunMotor = 2017,
                    UserEmail = "SEEDER"
                }));

                //tipe KLX 250 from 2006 - 2024
                currTipeKLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkKLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "KLX 250",
                    StartTahunMotor = 2006,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeKLX.Where(w => !allTipeListDbKLX.Select(s => new
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

            //Add W175 type & year
            var checkW175ModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "W175").FirstOrDefault();
            if (checkW175ModelExist != null)
            {
                var currTipeW175 = new List<MasterTipeMotor>();

                var allTipeListDbW175 = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkW175ModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe W175 from 2017 - 2024
                currTipeW175.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkW175ModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "W175",
                    StartTahunMotor = 2017,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeW175.Where(w => !allTipeListDbW175.Select(s => new
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

        private static void AddKawasakiComponent(InspectionDatabaseContext context, long? MerkMotorId)
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
        private static void AddKawasakiTahunType(InspectionDatabaseContext context, long? MerkMotorId)
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
