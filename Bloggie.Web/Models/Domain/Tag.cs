namespace Bloggie.Web;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }

    //Creating a many to many relationship with BlogPost
    public ICollection<BlogPost> BlogPosts { get; set; }
}
