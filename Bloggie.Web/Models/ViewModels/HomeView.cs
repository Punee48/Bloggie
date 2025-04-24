namespace Bloggie.Web.Models.ViewModels;

public class HomeView
{
    public IEnumerable<BlogPost> BlogPost { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}
