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
            var tag = _bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            //Mapping the EditTagRequest to Tag Domain Models and updating the database
            var tags = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            //Finding the tag in the database and updating it If the tag is no found then redirecting to the Edit page
            //If the tag is found then updating the tag and saving the changes to the database
            var updatedTag = _bloggieDbContext.Tags.Find(tags.Id);
            if (updatedTag != null)
            {
                updatedTag.Name = tags.Name;
                updatedTag.DisplayName = tags.DisplayName;
                _bloggieDbContext.SaveChanges();
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tags = _bloggieDbContext.Tags.Find(editTagRequest.Id);
            if (tags != null)
            {
                _bloggieDbContext.Tags.Remove(tags);
                _bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return View("Edit", new { id = editTagRequest.Id });
        }

    }
}
