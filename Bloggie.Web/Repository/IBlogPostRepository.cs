namespace Bloggie.Web;

public interface IBlogPostRepository
{
     Task<IEnumerable<BlogPost>> GetAllSync();
     Task<BlogPost?> GetAsync(Guid id);
     Task<BlogPost> AddAsync(BlogPost blogPost);
     Task<BlogPost?> UpdateAsync(BlogPost blogPost);
     Task<BlogPost?> DeleteAsync(Guid id);
     Task<BlogPost?> GetByUrlHandle(string urlHandle);
}
