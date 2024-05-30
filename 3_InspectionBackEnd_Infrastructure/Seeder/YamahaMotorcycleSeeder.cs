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
    public static class YamahaMotorcycleSeeder
    {
        public static void YamahaMotorcycleSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddYamaha(context);
            Console.Clear();
        }
        private static void AddYamaha(InspectionDatabaseContext context)
        {
            //Check jika ada Merk yamaha di DB
            var checkYamahaExist = context.MasterMerkMotors.Where(w => w.NAMA_MERK_MOTOR == "Yamaha").FirstOrDefault();
            if (checkYamahaExist == null)
            {
                //Add Ketika tidak ada
                var newlyCreatedModel = MasterMerkMotor.CreateMerkMotor(new MasterMerkMotorEntity
                {
                    NamaMerkMotor = "Yamaha",
                    UserEmail = "SEEDER"
                });
                context.MasterMerkMotors.Add(newlyCreatedModel);
                context.SaveChanges();

                checkYamahaExist = newlyCreatedModel;
            }

            //Tambahkan model untuk merk yamaha
            AddYamahaModels(context, checkYamahaExist.MERK_MOTOR_ID, checkYamahaExist.NAMA_MERK_MOTOR);

            context.SaveChanges();
        }
        private static void AddYamahaModels(InspectionDatabaseContext context, long? MerkMotorId, string? namaMerkMotor)
        {
            //Tambahkan model Aerox jika tidak ada di database
            var checkAeroxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Aerox").FirstOrDefault();
            if (checkAeroxModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Aerox",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Nmax jika tidak ada di database
            var checkNmaxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Nmax").FirstOrDefault();
            if (checkNmaxModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Nmax",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Mio jika tidak ada di database
            var checkMioModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Mio").FirstOrDefault();
            if (checkMioModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Mio",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Xmax jika tidak ada di database
            var checkXmaxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Xmax").FirstOrDefault();
            if (checkXmaxModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Xmax",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkkan model motor lain jika tidak ada
            //
            context.SaveChanges();

            //Tambahkan tipe motor tergantung merk dan modelnya
            AddYamahaType(context, namaMerkMotor);

            //Tambahkan tahun tipe motor tergantung dengan tipe dan tahunnya
            AddYamahaTahunType(context, MerkMotorId);

            //Tambahkan Component pada tiap model motor
            AddYamahaComponent(context, MerkMotorId);
        }

        private static void AddYamahaType(InspectionDatabaseContext context, string? namaMerkMotor)
        {
            //Add Aerox type & year
            var checkAeroxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Aerox").FirstOrDefault();
            if (checkAeroxModelExist != null)
            {
                var currTipeAerox = new List<MasterTipeMotor>();

                var allTipeListDbAerox = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkAeroxModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Aerox from 2016 - 2020
                currTipeAerox.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkAeroxModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Aerox",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2020,
                    UserEmail = "SEEDER"
                }));

                //tipe Aerox Connected from 2020 - 2024
                currTipeAerox.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkAeroxModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Aerox Connected",
                    StartTahunMotor = 2020,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeAerox.Where(w => !allTipeListDbAerox.Select(s => new
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

            //Add Nmax type & year
            var checkNmaxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Nmax").FirstOrDefault();
            if (checkNmaxModelExist != null)
            {
                var currTipeNmax = new List<MasterTipeMotor>();

                var allTipeListDbNmax = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkNmaxModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Nmax from 2015 - 2019
                currTipeNmax.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkNmaxModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Nmax",
                    StartTahunMotor = 2015,
                    EndTahunMotor = 2019,
                    UserEmail = "SEEDER"
                }));

                //tipe Nmax from 2019 - 2024
                currTipeNmax.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkNmaxModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Nmax Connected",
                    StartTahunMotor = 2019,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeNmax.Where(w => !allTipeListDbNmax.Select(s => new
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

            //Add Mio type & year
            var checkMioModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Mio").FirstOrDefault();
            if (checkMioModelExist != null)
            {
                var currTipeMio = new List<MasterTipeMotor>();

                var allTipeListDbMio = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkMioModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Mio from 2003 - 2008
                currTipeMio.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkMioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Mio",
                    StartTahunMotor = 2003,
                    EndTahunMotor = 2008,
                    UserEmail = "SEEDER"
                }));

                //tipe Mio Soul from 2007 - 2012
                currTipeMio.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkMioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Mio Soul",
                    StartTahunMotor = 2007,
                    EndTahunMotor = 2012,
                    UserEmail = "SEEDER"
                }));

                //tipe Mio Soul GT from 2012 - 2024
                currTipeMio.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkMioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Mio Soul GT",
                    StartTahunMotor = 2012,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                //tipe Mio J from 2015 - 2019
                currTipeMio.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkMioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Mio J",
                    StartTahunMotor = 2015,
                    EndTahunMotor = 2019,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeMio.Where(w => !allTipeListDbMio.Select(s => new
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

            //Add Xmax type & year
            var checkXmaxModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Xmax").FirstOrDefault();
            if (checkXmaxModelExist != null)
            {
                var currTipeXmax = new List<MasterTipeMotor>();

                var allTipeListDbXmax = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkXmaxModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Xmax from 2017 - 2024
                currTipeXmax.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkXmaxModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Xmax",
                    StartTahunMotor = 2017,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeXmax.Where(w => !allTipeListDbXmax.Select(s => new
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

        private static void AddYamahaComponent(InspectionDatabaseContext context, long? MerkMotorId)
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
        private static void AddYamahaTahunType(InspectionDatabaseContext context, long? MerkMotorId)
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
