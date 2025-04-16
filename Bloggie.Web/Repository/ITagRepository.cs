namespace Bloggie.Web;

public interface ITagRepository
{
    //Repository acts has a bridge between the application and the database
    //It is used to perform CRUD operations on the database
    //It helps to hide the Database implementation details from the application and no other class can access the database directly

    //Interface is used for defination of methods and properties

    Task<IEnumerable<Tag>> GetAllSync();
    Task<Tag?> GetAsync(Guid id);
    Task<Tag?> AddAsync(Tag tag);
    Task<Tag?> UpdateAsync(Tag tag);
    Task<Tag?> DeleteAsync(Guid id);

}
