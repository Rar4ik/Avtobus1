using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avtobus1.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Avtobus1.DAL.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UrlEntity> stringUrl { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
