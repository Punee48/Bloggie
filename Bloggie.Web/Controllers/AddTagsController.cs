using Bloggie.Web;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class AddTagsController : Controller
    {
        private readonly BloggieDbContext _bloggieDbContext;
        //Constructor to initialize the BloggieDbContext

        public AddTagsController(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }
        // GET: AddTagsController
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequests addTagRequests)
        {
            //Mapping the AddTagRequests to Tag Domain Models
            var tag = new Tag
            {
                Name = addTagRequests.Name,
                DisplayName = addTagRequests.DisplayName
            };
            _bloggieDbContext.Tags.Add(tag);
            _bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //Fetching all the tags from the database and passing it to the view
            var allTags = _bloggieDbContext.Tags.ToList();
            return View(allTags);
        }

        [HttpGet]

        public IActionResult Edit(Guid id)
        {
            
            return View();
        }

    }
}
