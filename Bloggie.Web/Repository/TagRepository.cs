
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web;

public class TagRepository : ITagRepository
{
    //Injecting the BloggieDbContext into the TagRepository class
    //This allows the TagRepository class to use the BloggieDbContext to interact with the database 

    private readonly BloggieDbContext _bloggieDbContext;

    public TagRepository(BloggieDbContext bloggieDbContext)
    {
        this._bloggieDbContext = bloggieDbContext;
    }
    public async Task<Tag?> AddAsync(Tag tag)
    {
        await _bloggieDbContext.AddAsync(tag);
        await _bloggieDbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> DeleteAsync(Guid id)
    {
        var existingTag = await _bloggieDbContext.Tags.FindAsync(id);
        if (existingTag != null)
        {
            _bloggieDbContext.Tags.Remove(existingTag);
            await _bloggieDbContext.SaveChangesAsync();
            return existingTag;
        }
        return null;
    }

    public async Task<IEnumerable<Tag>> GetAllSync()
    {
        return await _bloggieDbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(Guid id)
    {
        return await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        //Finding the tag in the database and updating it If the tag is no found then redirecting to the Edit page
        //If the tag is found then updating the tag and saving the changes to the database
        var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

        if (existingTag != null)
        {
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;

            await _bloggieDbContext.SaveChangesAsync();
            return existingTag;
        }
        else
        {
            return null;
        }
    }
}
