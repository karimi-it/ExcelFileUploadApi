using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Application.Interfaces
{
    public interface IExcelService
    {
        public Task ProcessExcelFileAsync(byte[] fileData);
        public Task test();
    }
}
