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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.Seeder
{
    public static class HondaMotorcycleSeeder
    {
        public static void HondaMotorcycleSeed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<InspectionDatabaseContext>();
            context.Database.EnsureCreated();
            AddHonda(context);
            Console.Clear();
        }
        private static void AddHonda(InspectionDatabaseContext context)
        {
            //Check jika ada Merk Honda di DB
            var checkHondaExist = context.MasterMerkMotors.Where(w => w.NAMA_MERK_MOTOR == "Honda").FirstOrDefault();
            if (checkHondaExist == null)
            {
                //Add Ketika tidak ada
                var newlyCreatedModel = MasterMerkMotor.CreateMerkMotor(new MasterMerkMotorEntity
                {
                    NamaMerkMotor = "Honda",
                    UserEmail = "SEEDER"
                });
                context.MasterMerkMotors.Add(newlyCreatedModel);
                context.SaveChanges();

                checkHondaExist = newlyCreatedModel;
            }

            //Tambahkan model untuk merk honda
            AddHondaModels(context, checkHondaExist.MERK_MOTOR_ID, checkHondaExist.NAMA_MERK_MOTOR);
            context.SaveChanges();
        }
        private static void AddHondaModels(InspectionDatabaseContext context, long? MerkMotorId, string? namaMerkMotor)
        {
            //Tambahkan model vario jika tidak ada di database
            var checkVarioModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vario").FirstOrDefault();
            if (checkVarioModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Vario",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model revo jika tidak ada di database
            var checkRevoModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Revo").FirstOrDefault();
            if (checkRevoModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Revo",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model blade jika tidak ada di database
            var checkBladeModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Blade").FirstOrDefault();
            if (checkBladeModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Blade",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model beat jika tidak ada di database
            var checkBeatModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Beat").FirstOrDefault();
            if (checkBeatModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Beat",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model CB jika tidak ada di database
            var checkCBModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "CB").FirstOrDefault();
            if (checkCBModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "CB",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model CBR jika tidak ada di database
            var checkCBRModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "CBR").FirstOrDefault();
            if (checkCBRModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "CBR",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model pcx jika tidak ada di database
            var checkPCXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "PCX").FirstOrDefault();
            if (checkPCXModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "PCX",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model sonic jika tidak ada di database
            var checkSonicModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Sonic").FirstOrDefault();
            if (checkSonicModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Sonic",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model spacy jika tidak ada di database
            var checkSpacyModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Spacy").FirstOrDefault();
            if (checkSonicModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Spacy",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkan model supra jika tidak ada di database
            var checkSupraModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Supra").FirstOrDefault();
            if (checkSonicModelExist == null)
            {
                context.MasterModelMotors.Add(MasterModelMotor.CreateModelMotor(new MasterModelMotorEntity
                {
                    MerkMotorId = MerkMotorId,
                    NamaModelMotor = "Supra",
                    UserEmail = "SEEDER"
                }));
            }

            //Tambahkkan model motor lain jika tidak ada
            //
            context.SaveChanges();

            //Tambahkan tipe motor tergantung merk dan modelnya
            AddHondaType(context, namaMerkMotor);

            //Tambahkan tahun tipe motor tergantung dengan tipe dan tahunnya
            AddHondaTahunType(context, MerkMotorId);

            //Tambahkan Component pada tiap model motor
            AddHondaComponent(context, MerkMotorId);

        }

        private static void AddHondaType(InspectionDatabaseContext context, string? namaMerkMotor)
        {
            //Add vario type & year
            var checkVarioModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Vario").FirstOrDefault();
            if (checkVarioModelExist != null)
            {
                var currTipeVario = new List<MasterTipeMotor>();
                var allTipeListDbVario = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkVarioModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //tipe vario 110 from 2006 - 2013
                currTipeVario.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVarioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vario 110",
                    StartTahunMotor = 2006,
                    EndTahunMotor = 2013,
                    UserEmail = "SEEDER"
                }));

                //loop tipe vario 125 from 2012 - 2015

                currTipeVario.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVarioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vario 125",
                    StartTahunMotor = 2012,
                    EndTahunMotor = 2015,
                    UserEmail = "SEEDER"
                }));

                //loop tipe vario 125 from 2016 - 2024
                currTipeVario.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVarioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "New Vario 125",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                //loop tipe vario 150 from 2016 - 2024
                currTipeVario.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVarioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Vario 150",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                //loop tipe vario 160 from 2016 - 2024
                currTipeVario.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkVarioModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "New Vario 160",
                    StartTahunMotor = 2016,
                    EndTahunMotor = 2024,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeVario.Where(w => !allTipeListDbVario.Select(s => new
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
            }

            //Add revo type & year
            var checkRevoModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Revo").FirstOrDefault();
            if (checkRevoModelExist != null)
            {
                var currTipeRevo = new List<MasterTipeMotor>();
                var allTipeListDbRevo = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkRevoModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //loop tipe revo from 2007 - 2010
                currTipeRevo.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkRevoModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Revo",
                    StartTahunMotor = 2007,
                    EndTahunMotor = 2010,
                    UserEmail = "SEEDER"
                }));

                //loop tipe absolute revo from 2010 - 2013
                currTipeRevo.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkRevoModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Absolute Revo",
                    StartTahunMotor = 2010,
                    EndTahunMotor = 2013,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeRevo.Where(w => !allTipeListDbRevo.Select(s => new
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
            }

            //Add Blade type & year
            var checkBladeModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Blade").FirstOrDefault();
            if (checkBladeModelExist != null)
            {
                var currTipeBlade = new List<MasterTipeMotor>();

                var allTipeListDbBlade = context.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == checkBladeModelExist.MODEL_MOTOR_ID)
                    .ToList();

                //loop tipe blade 110 from 2008 - 2014
                currTipeBlade.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkBladeModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Blade 110",
                    StartTahunMotor = 2008,
                    EndTahunMotor = 2014,
                    UserEmail = "SEEDER"
                }));


                //loop tipe blade 125 from 2014 - 2020
                currTipeBlade.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                {
                    ModelMotorId = checkBladeModelExist.MODEL_MOTOR_ID,
                    NamaMerkMotor = namaMerkMotor,
                    NamaTipeMotor = "Blade 125",
                    StartTahunMotor = 2014,
                    EndTahunMotor = 2020,
                    UserEmail = "SEEDER"
                }));

                context.MasterTipeMotors.AddRange(
                    currTipeBlade.Where(w => !allTipeListDbBlade.Select(s => new
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

                //Add Beat type & year
                var checkBeatModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Beat").FirstOrDefault();
                if (checkBeatModelExist != null)
                {
                    var currTipeBeat = new List<MasterTipeMotor>();

                    var allTipeListDbBeat = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkBeatModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //loop tipe all new beat 110 from 2020 - 2024

                    currTipeBeat.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkBeatModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "All New Beat",
                        StartTahunMotor = 2020,
                        EndTahunMotor = 2024,
                        UserEmail = "SEEDER"
                    }));


                    //loop tipe beat fi from 2012 - 2020
                    currTipeBeat.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkBeatModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Beat FI",
                        StartTahunMotor = 2012,
                        EndTahunMotor = 2020,
                        UserEmail = "SEEDER"
                    }));


                    //loop tipe beat street from 2016 - 2020
                    currTipeBeat.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkBeatModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Beat Street",
                        StartTahunMotor = 2016,
                        EndTahunMotor = 2020,
                        UserEmail = "SEEDER"
                    }));


                    //loop tipe beat pop from 2014 - 2016
                    currTipeBeat.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkBeatModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Beat Pop",
                        StartTahunMotor = 2014,
                        EndTahunMotor = 2016,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipeBeat.Where(w => !allTipeListDbBeat.Select(s => new
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
                }

                //Add CB type & year
                var checkCBModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "CB").FirstOrDefault();
                if (checkCBModelExist != null)
                {
                    var currTipeCB = new List<MasterTipeMotor>();

                    var allTipeListDbCB = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkCBModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //loop tipe CB 150 R from 2012 - 2024
                    currTipeCB.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkCBModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "CB 150 R",
                        StartTahunMotor = 2012,
                        EndTahunMotor = 2024,
                        UserEmail = "SEEDER"
                    }));

                    context.MasterTipeMotors.AddRange(
                        currTipeCB.Where(w => !allTipeListDbCB.Select(s => new
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
                }

                //Add CBR type & year
                var checkCBRModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "CBR").FirstOrDefault();
                if (checkCBRModelExist != null)
                {
                    var currTipeCBR = new List<MasterTipeMotor>();
                    var allTipeListDbCBR = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkCBRModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //tipe CBR 150 from 2011 - 2018
                    currTipeCBR.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkCBRModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "CBR 150",
                        StartTahunMotor = 2011,
                        EndTahunMotor = 2018,
                        UserEmail = "SEEDER"
                    }));

                    //tipe CBR 250 from 2011 - 2024
                    currTipeCBR.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkCBRModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "CBR 250",
                        StartTahunMotor = 2011,
                        EndTahunMotor = 2024,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipeCBR.Where(w => !allTipeListDbCBR.Select(s => new
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

                }

                //Add PCX type & year
                var checkPCXModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "PCX").FirstOrDefault();
                if (checkPCXModelExist != null)
                {
                    var currTipePCX = new List<MasterTipeMotor>();

                    var allTipeListDbPCX = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkPCXModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //tipe PCX 150 from 2012 - 2020
                    currTipePCX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkPCXModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "PCX 150",
                        StartTahunMotor = 2012,
                        EndTahunMotor = 2020,
                        UserEmail = "SEEDER"
                    }));


                    //tipe PCX 160 from 2021 - 2024
                    currTipePCX.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkPCXModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "PCX 160",
                        StartTahunMotor = 2021,
                        EndTahunMotor = 2024,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipePCX.Where(w => !allTipeListDbPCX.Select(s => new
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
                }

                //Add Sonic type & year
                var checkSonicModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Sonic").FirstOrDefault();
                if (checkSonicModelExist != null)
                {
                    var currTipeSonic = new List<MasterTipeMotor>();

                    var allTipeListDbSonic = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkSonicModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //tipe Sonic 150 from 2015 - 2020
                    currTipeSonic.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSonicModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Sonic 150",
                        StartTahunMotor = 2015,
                        EndTahunMotor = 2020,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipeSonic.Where(w => !allTipeListDbSonic.Select(s => new
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
                }

                //Add Spacy type & year
                var checkSpacyModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Spacy").FirstOrDefault();
                if (checkSpacyModelExist != null)
                {
                    var currTipeSpacy = new List<MasterTipeMotor>();

                    var allTipeListDbSpacy = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkSpacyModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //tipe Spacy from 2011 - 2018
                    currTipeSpacy.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSpacyModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Spacy",
                        StartTahunMotor = 2011,
                        EndTahunMotor = 2018,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipeSpacy.Where(w => !allTipeListDbSpacy.Select(s => new
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

                }

                //Add Supra type & year
                var checkSupraModelExist = context.MasterModelMotors.Where(w => w.NAMA_MODEL_MOTOR == "Supra").FirstOrDefault();
                if (checkSupraModelExist != null)
                {
                    var currTipeSupra = new List<MasterTipeMotor>();

                    var allTipeListDbSupra = context.MasterTipeMotors
                        .Where(w => w.MODEL_MOTOR_ID == checkSupraModelExist.MODEL_MOTOR_ID)
                        .ToList();

                    //tipe Supra X from 2005 - 2013
                    currTipeSupra.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSupraModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Supra X",
                        StartTahunMotor = 2005,
                        EndTahunMotor = 2013,
                        UserEmail = "SEEDER"
                    }));


                    //tipe Supra X Fi from 2014 - 2023
                    currTipeSupra.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSupraModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Supra X Fi",
                        StartTahunMotor = 2014,
                        EndTahunMotor = 2023,
                        UserEmail = "SEEDER"
                    }));


                    //tipe Supra GTR from 2017 - 2023
                    currTipeSupra.Add(MasterTipeMotor.CreateTipeMotor(new MasterTipeMotorEntity
                    {
                        ModelMotorId = checkSupraModelExist.MODEL_MOTOR_ID,
                        NamaMerkMotor = namaMerkMotor,
                        NamaTipeMotor = "Supra GTR",
                        StartTahunMotor = 2017,
                        EndTahunMotor = 2023,
                        UserEmail = "SEEDER"
                    }));


                    context.MasterTipeMotors.AddRange(
                        currTipeSupra.Where(w => !allTipeListDbSupra.Select(s => new
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
                }
            }

            context.SaveChanges();
        }

        private static void AddHondaComponent(InspectionDatabaseContext context, long? MerkMotorId)
        {
            //Tambahkan komponen
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

        private static void AddHondaTahunType(InspectionDatabaseContext context, long? MerkMotorId)
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
