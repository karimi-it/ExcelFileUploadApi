using ExcelFileUploadApi.Application.Interfaces;
using ExcelFileUploadApi.Application.Services;
using ExcelFileUploadApi.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Application.Tests
{
    public class ExcelServiceTests
    {
        private readonly Mock<IExcelRecordRepository> _mockRepository;
        private readonly IExcelService _excelService;

        public ExcelServiceTests()
        {
            _mockRepository = new Mock<IExcelRecordRepository>();
            _excelService = new ExcelService(_mockRepository.Object);
        }

        [Fact]
        public async Task ProcessExcelFileAsync_ShouldSaveRecords()
        {
            // Arrange
         //   var stream = new MemoryStream(); // اینجا یک Stream نمونه برای تست ایجاد کنید

            // Act
        //    await _excelService.ProcessExcelFileAsync(stream);

            // Assert
          //  _mockRepository.Verify(r => r.AddRecordsAsync(It.IsAny<IEnumerable<ExcelRecord>>()), Times.Once);
        }
    }
}
