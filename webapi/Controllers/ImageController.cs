using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace webapi.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImagesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            string connectionString = _configuration["AzureStorage:ConnectionString"];
            string containerName = _configuration["AzureStorage:BlobContainerName"];

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(imageName);

            if (await blob.ExistsAsync())
            {
                var memoryStream = new MemoryStream();
                await blob.DownloadToStreamAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "image/jpeg");
            }

            return NotFound();
        }
    }

}
