using ExcelFileUploadApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUploadApi.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ExcelRecord> ExcelRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
        }
    }
}
