using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web;

public class UserRepository : IUserRepository
{

    //Inject the Auth DB
    private readonly AuthDbContext _authDbContext;

    public UserRepository(AuthDbContext authDbContext)
    {
        this._authDbContext = authDbContext;
    }
    public async Task<IEnumerable<IdentityUser>> GetAllUser()
    {
        var users = await _authDbContext.Users.ToListAsync();
        var superAdmin = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

        if(superAdmin != null)
        {
            users.Remove(superAdmin);
        }

        return users;
    }
}
