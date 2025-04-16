using Bloggie.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Namespace
{
    public class AdminBlogPostController : Controller
    {

        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)

        {
            this._tagRepository = tagRepository;
            this._blogPostRepository = blogPostRepository;
        }

        // GET: AdminBlogPostController
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllSync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.DisplayName,
                    Value = x.Id.ToString()
                })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Mapping the AddBlogPostRequest to BlogPost Domain Models

            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageURL = addBlogPostRequest.FeaturedImageURL,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                IsVisible = addBlogPostRequest.IsVisible
            };

            var selectedTags = new List<Tag>();
            //Adding the selected tags to the blog post
            foreach (var select in addBlogPostRequest.selectedTags)
            {
                var selectecTagGuidId = Guid.Parse(select);
                var exisitingTag = await _tagRepository.GetAsync(selectecTagGuidId);
                if (exisitingTag != null)
                {
                    selectedTags.Add(exisitingTag);
                }

            }
            //mapping the selected tags to the blog post
            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostRepository.GetAllSync();
            return View(blogPosts);
        }

    }
}
