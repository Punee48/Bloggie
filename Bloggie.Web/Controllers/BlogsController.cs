using Bloggie.Web;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }
        // GET: BlogsController
        [HttpGet]
        public async Task<ActionResult> Index(string urlHandle)
        {
            var blogPosts = await _blogPostRepository.GetByUrlHandle(urlHandle);
            return View(blogPosts);
        }

    }
}
