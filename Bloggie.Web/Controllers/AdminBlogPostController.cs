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

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {

            var getBlogPost = await _blogPostRepository.GetAsync(id);

            var tags = await _tagRepository.GetAllSync();

            //We need to map the BlogPost to EditBlogPostRequest

            if (getBlogPost != null)
            {
                var editBlogPost = new EditBlogPostRequest
                {
                    Id = getBlogPost.Id,
                    Heading = getBlogPost.Heading,
                    PageTitle = getBlogPost.PageTitle,
                    Content = getBlogPost.Content,
                    ShortDescription = getBlogPost.ShortDescription,
                    FeaturedImageURL = getBlogPost.FeaturedImageURL,
                    UrlHandle = getBlogPost.UrlHandle,
                    PublishedDate = getBlogPost.PublishedDate,
                    Author = getBlogPost.Author,
                    IsVisible = getBlogPost.IsVisible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.DisplayName,
                        Value = x.Id.ToString(),
                    }),
                    selectedTags = getBlogPost.Tags.Select(x => x.Id.ToString()).ToArray()

                };

                return View(editBlogPost);
            }


            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {

            //Mapping the EditBlogPostRequest to BlogPost Domain Models
            var editBlogPost = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageURL = editBlogPostRequest.FeaturedImageURL,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author,
                IsVisible = editBlogPostRequest.IsVisible,

            };

            //Mapping the tags to the domain model

            var selectedTags = new List<Tag>();

            foreach (var selectedTag in editBlogPostRequest.selectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            editBlogPost.Tags = selectedTags;
            //Submit Information to the repository to update 

            var updateBlogPost = await _blogPostRepository.UpdateAsync(editBlogPost);

            if (updateBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletedBlogPost = await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

            if(deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
        }

    }
}
