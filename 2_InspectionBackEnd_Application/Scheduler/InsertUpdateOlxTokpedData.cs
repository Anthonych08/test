using _2_InspectionBackEnd_Application.Interfaces;
using System;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_InspectionBackEnd_Application.Extensions;
using System.Text.RegularExpressions;
using _1_InspectionBackEnd_Domain.Transaction;

namespace _2_InspectionBackEnd_Application.Scheduler
{
    [DisallowConcurrentExecution]
    public class InsertUpdateOlxTokpedData : IJob
    {
        private readonly IInspection_Datasource _inspection_DbContext;

        public InsertUpdateOlxTokpedData(
            IInspection_Datasource inspection_DbContext
            )
        {
            _inspection_DbContext = inspection_DbContext;
        }

        public Task Execute(IJobExecutionContext context)
        {

            //Update all price in komponen model motor
            var allKomponenModelInDatabase = _inspection_DbContext.KomponenModelMotors.Where(w => w.SEARCH_KEYWORD != null).ToList();
            var allSearchKomponenModelInDatabase = allKomponenModelInDatabase.Select(w => w.SEARCH_KEYWORD).ToList();
            var needToUpdateKomponenModelDb = new List<KomponenModelMotor>();
            var scrapedKomponenPrices = WebScrapeExtensionV2.Tokopedia(allSearchKomponenModelInDatabase);
            foreach (var komponenModel in allKomponenModelInDatabase)
            {
                var currKomponenModelPrice = scrapedKomponenPrices.Where(w => w.SearchKeyword == komponenModel.SEARCH_KEYWORD).FirstOrDefault();
                if (currKomponenModelPrice != null)
                {
                    komponenModel.UpdateHargaKomponenModel(new KomponenModelMotorEntity
                    {
                        HargaKomponen = currKomponenModelPrice.Price,
                        UserEmail = "SCHEDULER"
                    });
                    needToUpdateKomponenModelDb.Add(komponenModel);
                };
            };
            _inspection_DbContext.KomponenModelMotors.UpdateRange(needToUpdateKomponenModelDb);
            _inspection_DbContext.SaveChanges();

            //Update all price in master tipe motor
            var allTahunTypeInDatabase = _inspection_DbContext.TahunTipeMotors.Where(w => w.SEARCH_KEYWORD != null).ToList();
            var allSearchTahunTypeInDatabase = allTahunTypeInDatabase.Select(w => w.SEARCH_KEYWORD).ToList();
            var needToUpdateMotorTypeDb = new List<TahunTipeMotor>();
            var scrapedMotorPrices = WebScrapeExtensionV2.Olx(allSearchTahunTypeInDatabase);
            foreach (var tahunType in allTahunTypeInDatabase)
            {
                var currMotorTypePrice = scrapedMotorPrices.Where(w => w.SearchKeyword == tahunType.SEARCH_KEYWORD).FirstOrDefault();
                if (currMotorTypePrice != null)
                {
                    tahunType.UpdateHargaTipeTahunMotor(new TahunTipeMotorEntity
                    {
                        HargaMotorOlx = currMotorTypePrice.Price,
                        UserEmail = "SCHEDULER"
                    });
                    needToUpdateMotorTypeDb.Add(tahunType);
                }
            }
            _inspection_DbContext.TahunTipeMotors.UpdateRange(needToUpdateMotorTypeDb);

            _inspection_DbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
