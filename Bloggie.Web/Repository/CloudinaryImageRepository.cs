
using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web;

public class CloudinaryImageRepository : IImageRepository
{
    
    private readonly IConfiguration _configuration;
    private readonly Account _account;
    public CloudinaryImageRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
        _account = new Account(
            _configuration.GetSection("Cloudinary")["CloudName"],
            _configuration.GetSection("Cloudinary")["ApiKey"],
            _configuration.GetSection("Cloudinary")["ApiSecret"]
        );
    }
    public async Task<string> uploadAsync(IFormFile formFile)
    {
        var client = new Cloudinary(_account);
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
            DisplayName = formFile.FileName
        };

        var uploadResult = await client.UploadAsync(uploadParams);

        if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return uploadResult.SecureUri.ToString();
        }

        return null;
    }
}
