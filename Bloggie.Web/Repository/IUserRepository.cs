using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAllUser();

}
