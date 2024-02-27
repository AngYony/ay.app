using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Entities.Account;
using WY.Entities.BBS;
using WY.Entities.MockSchool;

namespace WY.EntityFramework
{
    public class WYDbContext : DbContext
    {
        public WYDbContext()
        {

        }
        public WYDbContext(DbContextOptions<WYDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Article>().ToTable("Article", "wy");
            //防止直接使用DbSet属性名作为表名，重新指定表名
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<StudentCourse>().ToTable("StudentCourse");



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
