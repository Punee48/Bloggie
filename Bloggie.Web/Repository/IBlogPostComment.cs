using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;

namespace Bloggie.Web;

public interface IBlogPostComment
{
    Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);

    Task<IEnumerable<BlogPostComment>> GetBlogCommentsById(Guid blogPostId);

}
