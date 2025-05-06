using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web;

public class BlogPostComments : IBlogPostComment
{
    private readonly BloggieDbContext _bloggieDbContext;
    public BlogPostComments(BloggieDbContext bloggieDbContext)
    {
        this._bloggieDbContext = bloggieDbContext;
    }
    public async Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment)
    {
        await _bloggieDbContext.BlogPostComments.AddAsync(blogPostComment);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPostComment;
    }

    public async Task<IEnumerable<BlogPostComment>> GetBlogCommentsById(Guid blogPostId)
    {
        return await _bloggieDbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
    }
}
