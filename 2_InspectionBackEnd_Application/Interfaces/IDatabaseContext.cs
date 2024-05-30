using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Interfaces
{
    public interface IDatabaseContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public DatabaseFacade Database { get; }
        int SaveChanges();
    }
}
