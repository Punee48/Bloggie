using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web;

public class BloggieDbContext : DbContext
{

    //This constructor is used to pass the options to the base class
    public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
    {
    }

    //Below Code help to create a database table for BlogPost and Tag
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogPostLike> BlogPostLikes { get; set; }

    public DbSet<BlogPostComment> BlogPostComments { get; set; }
}
