using ExcelFileUploadApi.Domain.Entities;
using ExcelFileUploadApi.Domain.Interfaces;
using ExcelFileUploadApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Infrastructure.Repositories
{
    public class ExcelRecordRepository : IExcelRecordRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExcelRecordRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRecordsAsync(IEnumerable<ExcelRecord> records)
        {
            await _dbContext.ExcelRecords.AddRangeAsync(records);
            await _dbContext.SaveChangesAsync();
        }

        public Task test()
        {
            try
            {
            _dbContext.ExcelRecords.Add(new ExcelRecord { Column1 ="sss" ,Column2 = "55555"});
            _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
            return Task.CompletedTask;
        }
    }
}
