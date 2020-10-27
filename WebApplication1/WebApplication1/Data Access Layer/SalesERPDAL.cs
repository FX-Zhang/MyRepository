using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Data_Access_Layer
{
    public class SalesERPDAL : DbContext
    {
        public DbSet<Employee> Employees { get; set; }                 //DbSet 指数据库中可以查询的实体的集合。表示数据库中能够被查询的所有Employee。  DbSet数据集是数据库方面的概念 ，指数据库中可以查询的实体的集合。当执行Linq 查询时，Dbset对象能够将查询内部转换，并触发数据库。
                                                                           


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Employee>().ToTable("TblEmployee");
          base.OnModelCreating(modelBuilder);
        }
        
    }
}