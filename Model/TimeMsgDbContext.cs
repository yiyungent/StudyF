using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TimeMsgDbContext : DbContext
    {
        public DbSet<TimeMsg> TimeMsg { get; set; }

        public TimeMsgDbContext(DbContextOptions<TimeMsgDbContext> options)
        : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = "server=localhost;user id=root;pwd=admin;database=StudyFCoreDB;";
        //    optionsBuilder.UseMySQL(connectionString);
        //}
    }
}
