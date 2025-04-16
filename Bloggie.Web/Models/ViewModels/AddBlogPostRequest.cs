using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web;

public class AddBlogPostRequest
{
    public string Heading { get; set; }
    public string PageTitle { get; set; }
    public string Content { get; set; }
    public string ShortDescription { get; set; }
    public string FeaturedImageURL { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; }
    public Boolean IsVisible { get; set; }

    //Creating a many to many relationship with BlogPost

    public IEnumerable<SelectListItem> Tags { get; set; }
    public string[] selectedTags { get; set; } = Array.Empty<string>();

}
