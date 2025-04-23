
using Microsoft.EntityFrameworkCore;

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

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var exisitingTag = await _bloggieDbContext.BlogPosts.FindAsync(id);
        if (exisitingTag != null)
        {
            _bloggieDbContext.BlogPosts.Remove(exisitingTag);
            await _bloggieDbContext.SaveChangesAsync();
            return exisitingTag;
        } 
        return null;
    }

    public async Task<IEnumerable<BlogPost>> GetAllSync()
    {
        return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
    }

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        return await _bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var exisitingTag = await _bloggieDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (exisitingTag != null)
        {
            exisitingTag.Id = blogPost.Id;
            exisitingTag.Heading = blogPost.Heading;
            exisitingTag.PageTitle = blogPost.PageTitle;
            exisitingTag.Content = blogPost.Content;
            exisitingTag.ShortDescription = blogPost.ShortDescription;
            exisitingTag.FeaturedImageURL = blogPost.FeaturedImageURL;
            exisitingTag.UrlHandle = blogPost.UrlHandle;
            exisitingTag.Tags = blogPost.Tags;
            exisitingTag.PublishedDate = blogPost.PublishedDate;
            exisitingTag.Author = blogPost.Author;
            exisitingTag.IsVisible = blogPost.IsVisible;

            await _bloggieDbContext.SaveChangesAsync();
            return exisitingTag;
        }

        return null;
    }
}
