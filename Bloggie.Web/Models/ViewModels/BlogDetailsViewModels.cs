namespace Bloggie.Web.Models.ViewModels;

public class BlogDetailsViewModels
{
    public Guid Id { get; set; }
    public string? Heading { get; set; }
    public string? PageTitle { get; set; }
    public string? Content { get; set; }
    public string? ShortDescription { get; set; }
    public string? FeaturedImageURL { get; set; }
    public string? UrlHandle { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? Author { get; set; }
    public Boolean IsVisible { get; set; }

    public ICollection<Tag> Tags { get; set; }
    public int TotalLikes { get; set; }

    public bool Liked { get; set; }
    public string Comments { get; set; }

    public IEnumerable<BlogCommentViewModel> CommentsDescription { get; set; }



}
