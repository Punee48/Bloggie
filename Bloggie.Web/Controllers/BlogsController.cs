using Bloggie.Web;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManger;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._signInManger = signInManager;
            this._userManager = userManger;
        }
        // GET: BlogsController
        [HttpGet]
        public async Task<ActionResult> Index(string urlHandle)
        {
            var blogPosts = await _blogPostRepository.GetByUrlHandle(urlHandle);

            var blogPostLike = new BlogDetailsViewModels();
            var liked = false;


            if (blogPosts != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPosts.Id);

                if(_signInManger.IsSignedIn(User))
                {
                    var blogLikes = await _blogPostLikeRepository.GetLikesForBlog(blogPosts.Id);

                    var userId = _userManager.GetUserId(User);

                    if(userId != null)
                    {
                        var blogLikeFromUser = blogLikes.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = blogLikeFromUser != null;

                    }
                }

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
                    TotalLikes = totalLikes,
                    Liked = liked
                };
            }
            return View(blogPostLike);
        }

    }
}
