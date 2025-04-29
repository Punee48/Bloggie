using Bloggie.Web;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
        }
        // GET: BlogsController
        [HttpGet]
        public async Task<ActionResult> Index(string urlHandle)
        {
            var blogPosts = await _blogPostRepository.GetByUrlHandle(urlHandle);

            var blogPostLike = new BlogDetailsViewModels();


            if (blogPosts != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPosts.Id);

                blogPostLike = new BlogDetailsViewModels
                {
                    Id = blogPosts.Id,
                    Heading = blogPosts.Heading,
                    PageTitle = blogPosts.PageTitle,
                    ShortDescription = blogPosts.ShortDescription,
                    FeaturedImageURL = blogPosts.FeaturedImageURL,
                    UrlHandle = blogPosts.UrlHandle,
                    PublishedDate = blogPosts.PublishedDate,
                    Author = blogPosts.Author,
                    IsVisible = blogPosts.IsVisible,
                    Tags = blogPosts.Tags,
                    TotalLikes = totalLikes
                };
            }
            return View(blogPostLike);
        }

    }
}
