using JMovies.IMDb.Entities.Common;
using JMovies.IMDb.Entities.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Utilities.Helpers
{
    public class PersonHelper
    {
        public static void WrapPersonImageUrls(Person person)
        {
            if (person != null)
            {
                ImageHelper.WrapImageUrl(person.PrimaryImage);
                if (person.Photos != null)
                {
                    foreach (Image image in person.Photos)
                    {
                        ImageHelper.WrapImageUrl(image);
                    }
                }
            }
        }
    }
}
