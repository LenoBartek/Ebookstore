using Microsoft.AspNetCore.Mvc;

namespace Ebookstore.Controllers
{
    [Route("ebooks")]
    [ApiController]
    public class GetDocController : ControllerBase
    {
        [HttpGet("GetPdf")]
        public async Task GetPdf(string fileName, CancellationToken cancellationToken)
        {
            Response.ContentType = "application/pdf";

            using (var stream = System.IO.File.OpenRead(Path.Combine("ebooks", fileName)))
            {
                await stream.CopyToAsync(Response.Body, cancellationToken);
            }
        }
    }
}
