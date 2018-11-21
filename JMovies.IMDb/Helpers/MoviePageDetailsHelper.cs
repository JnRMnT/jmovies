using HtmlAgilityPack;
using JMovies.Entities.IMDB;
using Fizzler.Systems.HtmlAgilityPack;
using JMovies.Common.Constants;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace JMovies.IMDb.Helpers
{
    public class MoviePageDetailsHelper
    {
        public static void Parse(Movie movie, HtmlNode detailsSection)
        {
            foreach (HtmlNode detailBox in detailsSection.QuerySelectorAll(".txt-block"))
            {
                HtmlNode headerNode = detailBox.QuerySelector("h4");
                if (headerNode != null)
                {
                    string headerContent = headerNode.InnerText.Prepare();
                    if (IMDbConstants.OfficialSitesHeaderRegex.IsMatch(headerContent))
                    {
                        List<OfficialSite> officialSites = new List<OfficialSite>();
                        Parallel.ForEach(detailBox.QuerySelectorAll("a"), (HtmlNode officialSiteLink) =>
                        {
                            string url = IMDbConstants.BaseURL + officialSiteLink.Attributes["href"].Value;
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.AllowAutoRedirect = false;
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                string redirectURL = response.Headers["Location"];
                                officialSites.Add(new OfficialSite
                                {
                                    Title = officialSiteLink.InnerText.Prepare(),
                                    URL = redirectURL
                                });
                            }
                        });
                        movie.OfficialSites = officialSites.ToArray();
                    }
                    else if (IMDbConstants.CountriesHeaderRegex.IsMatch(headerContent))
                    {
                        List<Country> countries = new List<Country>();
                        foreach (HtmlNode countryLink in detailBox.QuerySelectorAll("a"))
                        {
                            Match countryMatch = IMDbConstants.CountryOfOriginRegex.Match(countryLink.OuterHtml);
                            if (countryMatch.Success)
                            {
                                Country country = new Country
                                {
                                    Identifier = countryMatch.Groups[1].Value,
                                    Name = countryLink.InnerText.Prepare()
                                };
                                countries.Add(country);
                            }
                        }
                        movie.Countries = countries.ToArray();
                    }
                    else if (IMDbConstants.LanguagesHeaderRegex.IsMatch(headerContent))
                    {
                        List<Language> languages = new List<Language>();
                        foreach (HtmlNode languageLink in detailBox.QuerySelectorAll("a"))
                        {
                            Match languageMatch = IMDbConstants.PrimaryLanguageRegex.Match(languageLink.OuterHtml);
                            if (languageMatch.Success)
                            {
                                Language language = new Language
                                {
                                    Identifier = languageMatch.Groups[1].Value,
                                    Name = languageLink.InnerText.Prepare()
                                };
                                languages.Add(language);
                            }
                        }
                        movie.Languages = languages.ToArray();
                    }
                }
            }
        }
    }
}
