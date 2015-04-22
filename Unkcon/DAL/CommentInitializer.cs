using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Unkcon.Models;

namespace Unkcon.DAL
{
    public class CommentInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<CommentModel> comments = new List<CommentModel>
            {
                new CommentModel{Comment = "A comment"},
                new CommentModel{Comment = "Another comment"},
                new CommentModel{Comment = "Yet another comment"}
            };

            foreach (CommentModel comment in comments)
            {
                context.Comments.Add(comment);
            }
            context.SaveChanges();
        }
    }
}