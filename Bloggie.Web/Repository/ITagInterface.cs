namespace Bloggie.Web;

public interface ITagInterface
{
    //Declaring the methods to be implemented in the TagRepository class

    Task<IEnumerable<Tag>> GetAllTagsAsync();

    Task<Tag?> GetTagByIdAsync(Guid id);
    Task<Tag> AddAsync(Tag tag);
    Task<Tag?> UpdateAsync(Tag tag);
    Task<Tag?> DeleteAsync(Guid id);
}
