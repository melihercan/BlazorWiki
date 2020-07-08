using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Data
{
    public class ApplicationDbContext : DbContext
    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  //      {

    //    }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                    => options.UseSqlite("Data Source=Data/BlazorWiki.db");

        public DbSet<WikiPage> WikiPages { get; set; }
        public DbSet<WikiContent> WikiContents { get; set; }
    }
}
