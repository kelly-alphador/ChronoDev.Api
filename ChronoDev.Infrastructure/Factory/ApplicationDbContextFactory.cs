using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChronoDev.Infrastructure.Factory
{
    public class ApplicationDbContextFactory:IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer("Data Source=DESKTOP-1PCHEEU\\SQLEXPRESS;Initial Catalog=ChronoDev.Db;Integrated Security=True;Trust Server Certificate=True");
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}
