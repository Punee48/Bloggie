using Bloggie.Web;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        // GET: AdminUserController

        private readonly IUserRepository _userRepository;

        public AdminUserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAllUser();

            if (users != null)
            {
                var userViewModel = new UserViewModel();
                userViewModel.Users = new List<User>();

                foreach (var user in users)
                {
                    userViewModel.Users.Add(new Bloggie.Web.Models.ViewModels.User
                    {
                        Id = Guid.Parse(user.Id),
                        Username = user.UserName,
                        EmailAddress = user.Email

                    });
                }

                return View(userViewModel);
            }

            return View();
        }

    }
}
