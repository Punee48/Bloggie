using Bloggie.Web;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Models.Domain;
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

        private readonly IBlogPostComment _blogPostComment;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager, IBlogPostComment blogPostComment)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._signInManger = signInManager;
            this._userManager = userManger;
            this._blogPostComment = blogPostComment;
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

                if (_signInManger.IsSignedIn(User))
                {
                    var blogLikes = await _blogPostLikeRepository.GetLikesForBlog(blogPosts.Id);

                    var userId = _userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var blogLikeFromUser = blogLikes.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = blogLikeFromUser != null;

                    }
                }

                var blogPostComments = await _blogPostComment.GetBlogCommentsById(blogPosts.Id);

                var blogComment = new List<BlogCommentViewModel>();

                foreach (var comment in blogPostComments)
                {
                    blogComment.Add(new BlogCommentViewModel
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }

                blogPostLike = new BlogDetailsViewModels
                {
                    Id = blogPosts.Id,
                    Heading = blogPosts.Heading,
                    PageTitle = blogPosts.PageTitle,
                    Content = blogPosts.Content,
                    ShortDescription = blogPosts.ShortDescription,
                    FeaturedImageURL = blogPosts.FeaturedImageURL,
                    UrlHandle = blogPosts.UrlHandle,
                    PublishedDate = blogPosts.PublishedDate,
                    Author = blogPosts.Author,
                    IsVisible = blogPosts.IsVisible,
                    Tags = blogPosts.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    CommentsDescription = blogComment // Added this line to include comments in the view model
                };
            }
            return View(blogPostLike);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModels blogDetailsViewModels)
        {
            if (_signInManger.IsSignedIn(User))
            {
                var domain_Model = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModels.Id,
                    Description = blogDetailsViewModels.Comments,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await _blogPostComment.AddCommentAsync(domain_Model);
                return RedirectToAction("Index", "Home", new { UrlHandle = blogDetailsViewModels.UrlHandle });
            }
            else
            {
                return View();
            }

        }

    }
}
