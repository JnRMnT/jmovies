using JMovies.IMDb.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Web.Helpers
{
    public class ImageHelper
    {
        private static readonly string ImageControllerPath = "image/{0}";

        public static void WrapImageUrl(Image image)
        {
            if (image != null)
            {
                image.URL = string.Format(ImageControllerPath, image.ID);
            }
        }
    }
}
