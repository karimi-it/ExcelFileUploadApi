using ExcelFileUploadApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Domain.Interfaces
{
    public interface IExcelRecordRepository
    {
        Task AddRecordsAsync(IEnumerable<ExcelRecord> records);
        Task test();
    }
}
