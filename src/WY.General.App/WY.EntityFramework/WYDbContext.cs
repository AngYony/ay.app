using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Model.Models;

namespace WY.EntityFramework
{
    public class WYDbContext : DbContext
    {
        public WYDbContext(DbContextOptions<WYDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////将日志输出到控制台
            ////optionsBuilder.LogTo(Console.WriteLine);
            ////将日志输出到调试窗口
            //optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=192.168.52.131;database=Wy;User ID=sa;Password=abcd-1234");
            //}

            //base.OnConfiguring(optionsBuilder);
        }
    }
}
