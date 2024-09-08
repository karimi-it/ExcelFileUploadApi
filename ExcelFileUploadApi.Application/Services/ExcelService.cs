using ExcelDataReader;
using ExcelFileUploadApi.Application.Interfaces;
using ExcelFileUploadApi.Domain.Entities;
using ExcelFileUploadApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Application.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IExcelRecordRepository _repository;
        private readonly ILoggingService _loggingService;
        public ExcelService(IExcelRecordRepository repository,ILoggingService loggingService)
        {
            _repository = repository;
            _loggingService = loggingService;
        }

        public async Task ProcessExcelFileAsync(byte[] fileData)
        {
            const int batchSize = 10000; // اندازه دسته

            // تبدیل آرایه بایتی به MemoryStream
            using (var stream = new MemoryStream(fileData))
            {
                // استفاده از ExcelReaderFactory برای خواندن داده‌های اکسل
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[0];
                    List<ExcelRecord> records = new List<ExcelRecord>(batchSize);
                    var tasks = new List<Task>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var row = table.Rows[i];
                        var record = new ExcelRecord
                        {
                            Column1 = row[0]?.ToString(),
                            Column2 = row[1]?.ToString()
                        };
                        records.Add(record);

                        // اگر تعداد رکوردهای جمع‌آوری شده به اندازه دسته رسید، آنها را ذخیره کن
                        if (records.Count >= batchSize)
                        {
                            var batch = new List<ExcelRecord>(records);
                            tasks.Add(Task.Run(() => _repository.AddRecordsAsync(batch)));
                            records.Clear();
                        }
                    }

                    // ذخیره رکوردهای باقی‌مانده
                    if (records.Count > 0)
                    {
                        var batch = new List<ExcelRecord>(records);
                        tasks.Add(Task.Run(() => _repository.AddRecordsAsync(batch)));
                    }

                    // منتظر بمان تا همه دسته‌ها پردازش شوند
                    await Task.WhenAll(tasks);
                }
            }
        }

        public Task test()
        {
            return _repository.test();
        }
    }
}
