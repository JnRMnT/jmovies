using HtmlAgilityPack;
using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.IMDB;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;
using JMovies.IMDb.Helpers;

namespace JMovies.IMDb.Providers
{
    public class IMDbScraperDataProvider : IIMDbDataProvider
    {
        public Movie GetMovie(long id, bool fetchDetailedCast)
        {
            if (id == default(long))
            {
                throw new JMException("IMDbIDEmpty");
            }

            Movie movie = new Movie();
            string url = IMDbConstants.BaseURL + IMDbConstants.MoviesPath + IMDbConstants.MovieIDPrefix + id.ToString().PadLeft(IMDbConstants.IMDbIDLength, '0');
            HtmlDocument htmlDocument = new HtmlDocument();
            HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlElementFlag.Empty;
            htmlDocument.OptionWriteEmptyNodes = true;

            var webRequest = HttpWebRequest.Create(url);
            using (Stream stream = webRequest.GetResponse().GetResponseStream())
            {
                htmlDocument.Load(stream);
                stream.Close();
            }
            HtmlNode documentNode = htmlDocument.DocumentNode;

            //Parse and verify IMDb ID Meta Tag
            HtmlNode idMetaTag = documentNode.QuerySelector("meta[property='pageId']");
            if (idMetaTag != null)
            {
                movie.IMDbID = Regex.Replace(idMetaTag.Attributes["content"].Value, IMDbConstants.MovieIDPrefix, string.Empty).ToLong();
            }
            else
            {
                return null;
            }

            //Parse Title
            HtmlNode titleWrapper = documentNode.QuerySelector(".title_wrapper");
            if (titleWrapper != null)
            {
                movie.Title = titleWrapper.QuerySelector("h1").InnerText.Prepare();
                if (IMDbConstants.MovieYearRegex.IsMatch(movie.Title))
                {
                    Match yearMatch = IMDbConstants.MovieYearRegex.Match(movie.Title);
                    movie.Year = yearMatch.Groups[2].Value.Trim().ToInteger();
                    movie.Title = yearMatch.Groups[1].Value.Trim();
                }
                HtmlNode originalTitleNode = titleWrapper.QuerySelector(".originalTitle");
                if (originalTitleNode != null)
                {
                    movie.OriginalTitle = originalTitleNode.InnerText.Prepare();
                }
            }
            else
            {
                return null;
            }

            //Parse Summary
            HtmlNode summaryWrapper = documentNode.QuerySelector(".plot_summary_wrapper");
            List<Credit> credits = new List<Credit>();
            if (summaryWrapper != null)
            {
                HtmlNode summaryText = summaryWrapper.QuerySelector(".summary_text");
                if (summaryText != null)
                {
                    movie.PlotSummary = summaryText.InnerText.Prepare();
                }

                if (!fetchDetailedCast)
                {
                    foreach (HtmlNode creditSummaryNode in summaryWrapper.QuerySelectorAll(".credit_summary_item"))
                    {
                        Credit[] summaryCredits = SummaryCastHelper.GetCreditInfo(creditSummaryNode);
                        if (summaryCredits != null && summaryCredits.Length > 0)
                        {
                            credits.AddRange(summaryCredits);
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            //Parse Story Line
            HtmlNode storyLineSection = documentNode.QuerySelector("#titleStoryLine");
            if (storyLineSection != null)
            {
                SummaryStorylineHelper.Parse(movie, storyLineSection);
            }

            //Parse Details Section
            HtmlNode detailsSection = documentNode.QuerySelector("#titleDetails");
            if (detailsSection != null)
            {
                MoviePageDetailsHelper.Parse(movie, detailsSection);
            }

            if (!fetchDetailedCast)
            {
                //Parse Cast Table
                HtmlNode castListNode = documentNode.QuerySelector(".cast_list");
                if (castListNode != null)
                {
                    foreach (HtmlNode castNode in castListNode.QuerySelectorAll("tr"))
                    {
                        IEnumerable<HtmlNode> castColumns = castNode.QuerySelectorAll("td");
                        if (castColumns != null && castColumns.Count() == 4)
                        {
                            HtmlNode personNode = castColumns.ElementAt(1);
                            HtmlNode charactersNode = castColumns.ElementAt(3);

                            ActingCredit actingCredit = new ActingCredit();
                            actingCredit.Person = new Actor();
                            Match personIDMatch = IMDbConstants.PersonIDURLMatcher.Match(personNode.QuerySelector("a").Attributes["href"].Value);
                            if (personIDMatch.Success && personIDMatch.Groups.Count > 1)
                            {
                                actingCredit.Person.IMDbID = personIDMatch.Groups[1].Value.ToLong();
                                actingCredit.Person.FullName = personNode.InnerText.Prepare();
                            }

                            List<Character> characters = new List<Character>();
                            foreach (HtmlNode characterNode in charactersNode.QuerySelectorAll("a"))
                            {
                                characters.Add(GetCharacter(characterNode));
                            }
                            if (characters.Count == 0)
                            {
                                Character character = GetCharacter(charactersNode);
                                if (!string.IsNullOrEmpty(character.Name) || character.IMDbID != null)
                                {
                                    characters.Add(character);
                                }
                            }
                            actingCredit.Characters = characters.ToArray();
                            credits.Add(actingCredit);
                        }
                    }
                    movie.Credits = credits.ToArray();
                }
            }
            else
            {

            }

            return movie;
        }

        private static Character GetCharacter(HtmlNode characterNode)
        {
            Character character = new Character();
            character.Name = characterNode.InnerText.Prepare();
            if (IMDbConstants.CharacterRegex.IsMatch(characterNode.OuterHtml))
            {
                character.IMDbID = IMDbConstants.CharacterRegex.Match(characterNode.OuterHtml).Groups[1].Value.ToLong();
            }
            return character;
        }
    }
}