using Unkcon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Unkcon.DAL
{
    public class CommentContext : DbContext
    {
        public CommentContext()
            :base("CommentContext")
        {
        }

        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}