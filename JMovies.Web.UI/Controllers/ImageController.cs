using JMovies.Common.Constants;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;

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
        public ActionResult Get(long id, int w, int h)
        {
            GetImageContentsRequest request = new GetImageContentsRequest { ID = id };
            GetImageContentsResponse response = jmAppClientProvider.CallAction<GetImageContentsResponse>(ActionNameConstants.GetImageContents, request);

            if (response.Image == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                if (w != default(int) || h != default(int))
                {
                    using (Image image = Image.Load(response.Image.Content))
                    {
                        if (w != default(int))
                        {
                            h = (int)((double)image.Height / (double)image.Width * (double)w);
                        }
                        else
                        {
                            w = (int)((double)image.Width / (double)image.Height * (double)h);
                        }
                        ResizeOptions resizeOptions = new ResizeOptions
                        {
                            Size = new SixLabors.Primitives.Size
                            {
                                Width = w,
                                Height = h
                            }
                        };

                        using (MemoryStream stream = new MemoryStream())
                        {
                            image.Mutate(e => e.Resize(resizeOptions));
                            image.Save(stream, new JpegEncoder());
                            return new FileContentResult(stream.ToArray(), "image/jpeg");
                        }
                    }
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
}
