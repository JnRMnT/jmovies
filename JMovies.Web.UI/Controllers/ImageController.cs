using JMovies.Common.Constants;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Entities.People;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace JMovies.Controllers
{
    [Route("image")]
    public class ImageController : BaseApiController
    {
        private IJMAppClientProvider jmAppClientProvider;
        public ImageController(IJMAppClientProvider jmAppClientProvider) : base()
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            GetImageContentsRequest request = new GetImageContentsRequest { ID = id };
            GetImageContentsResponse response = jmAppClientProvider.CallAction<GetImageContentsResponse>(ActionNameConstants.GetImageContents, request);

            if (response.Image == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                string extension = Path.GetExtension(response.Image.URL).TrimStart('.');
                if (extension == "jpg")
                {
                    extension = "jpeg";
                }
                string mimeType = "image/" + extension;
                return new FileContentResult(response.Image.Content, mimeType);
            }
        }
    }
}
