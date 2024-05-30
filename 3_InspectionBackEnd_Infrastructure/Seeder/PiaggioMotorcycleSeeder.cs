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
    public static class PiaggioMotorcycleSeeder
    {
        public static void PiaggioMotorcycleSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddPiaggio(context);
            Console.Clear();
        }
        private static void AddPiaggio(InspectionDatabaseContext context)
        {
            //Check jika ada Merk Piaggio di DB
            var checkPiaggioExist = context.MasterMerkMotors.Where(w => w.NAMA_MERK_MOTOR == "Piaggio").FirstOrDefault();
            if (checkPiaggioExist == null)
            {
                //Add Ketika tidak ada
                var newlyCreatedModel = MasterMerkMotor.CreateMerkMotor(new MasterMerkMotorEntity
                {
                    NamaMerkMotor = "Piaggio",
                    UserEmail = "SEEDER"
                });
                context.MasterMerkMotors.Add(newlyCreatedModel);
                context.SaveChanges();
                checkPiaggioExist = newlyCreatedModel;
            };

            //Tambahkan model untuk merk piaggio
            AddPiaggioModels(context, checkPiaggioExist.MERK_MOTOR_ID, checkPiaggioExist.NAMA_MERK_MOTOR);

            context.SaveChanges();
        }
        private static void AddPiaggioModels(InspectionDatabaseContext context, long? MerkMotorId, string? namaMerkMotor)
        {
            //Tambahkan model Vespa LX jika tidak ada di database
            var checkVespaLXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa LX").FirstOrDefault();
            if (checkVespaLXModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vespa LX",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Vespa S jika tidak ada di database
            var checkVespaSModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa S").FirstOrDefault();
            if (checkVespaSModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vespa S",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Vespa Sprint jika tidak ada di database
            var checkVespaSprintModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa Sprint").FirstOrDefault();
            if (checkVespaSprintModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vespa Sprint",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Vespa Primavera jika tidak ada di database
            var checkVespaPrimaveraModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa Primavera").FirstOrDefault();
            if (checkVespaPrimaveraModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vespa Primavera",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model Vespa GTS jika tidak ada di database
            var checkVespaGTSModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa GTS").FirstOrDefault();
            if (checkVespaGTSModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vespa GTS",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkkan model motor lain jika tidak ada
            //
            context.SaveChanges();

            //Tambahkan tipe motor tergantung merk dan modelnya
            AddPiaggioType(context, namaMerkMotor);

            //Tambahkan tahun tipe motor tergantung dengan tipe dan tahunnya
            AddPiaggioTahunType(context, MerkMotorId);

            //Tambahkan Component pada tiap model motor
            AddPiaggioComponent(context, MerkMotorId);
        }

        private static void AddPiaggioType(InspectionDatabaseContext context, string? namaMerkMotor)
        {
            //Add Vespa LX type & year
            var checkVespaLXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "VespaLX").FirstOrDefault();
            if (checkVespaLXModelExist != null)
            {
                var currTipeVespaLX = new List<MasterTipeMotor>();

                var allTipeListDbVespaLX = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVespaLXModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe LX 125 from 2011 - 2012
                currTipeVespaLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "LX 125",
                    StartTahunMotor = 2011,
                    EndTahunMotor = 2012,
                    UserEmail = "SEEDER"
                }));

                //tipe LX 150 2V from 2011 - 2012
                currTipeVespaLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "LX 150 2V",
                    StartTahunMotor = 2011,
                    EndTahunMotor = 2012,
                    UserEmail = "SEEDER"
                }));

                //tipe LX 150 3V from 2013 - 2014
                currTipeVespaLX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaLXModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "LX 150 3V",
                    StartTahunMotor = 2013,
                    EndTahunMotor = 2014,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVespaLX.Where(w => !allTipeListDbVespaLX.Select(s => new
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

            //Add Vespa S type & year
            var checkVespaSModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa S").FirstOrDefault();
            if (checkVespaSModelExist != null)
            {
                var currTipeVespaS = new List<MasterTipeMotor>();

                var allTipeListDbVespaS = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVespaSModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Vespa S 2V from 2011 - 2012
                currTipeVespaS.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa S 2V",
                    StartTahunMotor = 2011,
                    EndTahunMotor = 2012,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa S 150 3V from 2013 - 2014
                currTipeVespaS.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa S 150 3V",
                    StartTahunMotor = 2013,
                    EndTahunMotor = 2014,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa S 125 3V from 2015 - 2017
                currTipeVespaS.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa S 125 3V",
                    StartTahunMotor = 2015,
                    EndTahunMotor = 2017,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVespaS.Where(w => !allTipeListDbVespaS.Select(s => new
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

            //Add Vespa Sprint type & year
            var checkVespaSprintModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa Sprint").FirstOrDefault();
            if (checkVespaSprintModelExist != null)
            {
                var currTipeVespaSprint = new List<MasterTipeMotor>();

                var allTipeListDbVespaSprint = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVespaSprintModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Vespa Sprint 3V from 2014 - 2016
                currTipeVespaSprint.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSprintModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Sprint 3V",
                    StartTahunMotor = 2014,
                    EndTahunMotor = 2016,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa Sprint Iget from 2016 - 2018
                currTipeVespaSprint.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSprintModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Sprint Iget",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2018,
                    UserEmail = "SEEDER"
                }));


                //tipe Vespa Sprint Iget Abs from 2018 - 2024
                currTipeVespaSprint.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaSprintModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Sprint Iget Abs",
                    StartTahunMotor = 2018,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVespaSprint.Where(w => !allTipeListDbVespaSprint.Select(s => new
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

            //Add Vespa Primavera type & year
            var checkVespaPrimaveraModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa Primavera").FirstOrDefault();
            if (checkVespaPrimaveraModelExist != null)
            {
                var currTipeVespaPrimavera = new List<MasterTipeMotor>();

                var allTipeListDbVespaPrimavera = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVespaPrimaveraModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Vespa Primavera 3V from 2014 - 2016
                currTipeVespaPrimavera.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaPrimaveraModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Primavera 3V",
                    StartTahunMotor = 2014,
                    EndTahunMotor = 2016,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa Primavera Iget from 2016 - 2018
                currTipeVespaPrimavera.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaPrimaveraModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Primavera Iget",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2018,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa Primavera Iget Abs from 2018 - 2024
                currTipeVespaPrimavera.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaPrimaveraModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa Primavera Iget Abs",
                    StartTahunMotor = 2018,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVespaPrimavera.Where(w => !allTipeListDbVespaPrimavera.Select(s => new
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

            //Add Vespa GTS type & year
            var checkVespaGTSModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vespa GTS").FirstOrDefault();
            if (checkVespaGTSModelExist != null)
            {
                var currTipeVespaGTS = new List<MasterTipeMotor>();

                var allTipeListDbVespaGTS = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVespaGTSModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe Vespa GTS 3V from 2014 - 2016
                currTipeVespaGTS.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaGTSModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa GTS 3V",
                    StartTahunMotor = 2014,
                    EndTahunMotor = 2016,
                    UserEmail = "SEEDER"
                }));

                //tipe Vespa GTS Iget from 2017 - 2024
                currTipeVespaGTS.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVespaGTSModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vespa GTS Iget",
                    StartTahunMotor = 2017,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVespaGTS.Where(w => !allTipeListDbVespaGTS.Select(s => new
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
        }

        private static void AddPiaggioComponent(InspectionDatabaseContext context, long? MerkMotorId)
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
        private static void AddPiaggioTahunType(InspectionDatabaseContext context, long? MerkMotorId)
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
