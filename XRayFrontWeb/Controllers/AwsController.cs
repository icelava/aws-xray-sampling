using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using XRayFrontWeb.Configuration;

namespace XRayFrontWeb.Controllers
{
	public class AwsController : Controller
	{
		// GET: AwsController
		public ActionResult Index()
		{
			return View();
		}

		// GET: AwsController/Image/<FILENAME WITHOUT .JPG>
		public async Task<IActionResult> Image(string id)
		{
			if (string.IsNullOrWhiteSpace(id)) return NotFound();

			var fileName = string.Format("{0}.jpg", id);

			Stream imgStream = await this.GetFileObject(fileName);
			if (imgStream == null) return NotFound();

			Response.Headers.Add("Content-Disposition", new ContentDisposition
			{
				FileName = fileName,
				Inline = true // false = prompt the user for downloading; true = browser to try to show the file inline
			}.ToString());

			return File(imgStream, "image/jpeg");
		}

		private async Task<Stream> GetFileObject(string fileName)
		{
			string filePath = string.Format("images/{0}", fileName);
			var s3Client = new AmazonS3Client(AwsConfig.Instance.AccessKeyId,
				AwsConfig.Instance.SecretAccessKey,
				RegionEndpoint.GetBySystemName(AwsConfig.Instance.Region));

			try
			{
				using (var objResponse = await s3Client.GetObjectAsync(AwsConfig.Instance.S3Bucket, filePath))
				{
					using (var responseStream = objResponse.ResponseStream)
					{
						var memStream = new MemoryStream();
						await responseStream.CopyToAsync(memStream);
						memStream.Position = 0;
						return memStream;
					}
				}
			}
			catch (AmazonS3Exception ex)
			{
				// Check if HTTP 404 Not found error.
				if (ex.StatusCode == HttpStatusCode.NotFound) return null;
				throw;
			}
		}
	}
}
