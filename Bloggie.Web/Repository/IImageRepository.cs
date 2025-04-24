namespace Bloggie.Web;

public interface IImageRepository
{
    Task<string> uploadAsync(IFormFile formFile);
}
