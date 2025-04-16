
namespace Bloggie.Web;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogPostRepository(BloggieDbContext bloggieDbContext)
    {
        this._bloggieDbContext = bloggieDbContext;
    }
    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _bloggieDbContext.BlogPosts.AddAsync(blogPost);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPost;
    }

    public Task<BlogPost?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BlogPost>> GetAllSync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        throw new NotImplementedException();
    }
}
