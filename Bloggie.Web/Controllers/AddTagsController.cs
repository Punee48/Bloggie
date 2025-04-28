using Bloggie.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class AddTagsController : Controller
    {
        
        private readonly ITagRepository _tagRepository;
        public AddTagsController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }
        // GET: AddTagsController
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequests addTagRequests)
        {
            //Mapping the AddTagRequests to Tag Domain Models
            var tag = new Tag
            {
                Name = addTagRequests.Name,
                DisplayName = addTagRequests.DisplayName
            };

            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Fetching all the tags from the database and passing it to the view
            var allTags = await _tagRepository.GetAllSync();
            return View(allTags);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //Mapping the EditTagRequest to Tag Domain Models and updating the database
            var tags = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await _tagRepository.UpdateAsync(tags);
            if (updatedTag != null)
            {
                //Show Success Message
            }
            else
            {
                //show error message
            }

            
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {

            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //Show Success Message
                return RedirectToAction("List");
            }
            else
            {
                //show error message
            }

            return View("Edit", new { id = editTagRequest.Id });
        }

    }
}
