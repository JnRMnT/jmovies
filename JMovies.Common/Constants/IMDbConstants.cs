﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JMovies.Common.Constants
{
    public class IMDbConstants
    {
        public static readonly int IMDbIDLength = 7;
        public static readonly string BaseURL = "https://www.imdb.com/";
        public static readonly string MoviesPath = "title/";
        public static readonly string MovieIDPrefix = "tt";
        public static readonly string PersonsPath = "name/";
        public static readonly string PersonIDPrefix = "nm";
        public static readonly string CharactersPath = "characters/";
        public static readonly string KeywordsPath = "keyword/";

        public static readonly string Star = "Star";
        public static readonly string Director = "Director";
        public static readonly string Writer = "Writer";
        public static readonly string Creator = "Creator";
        public static readonly string Tagline = "Tagline";
        public static readonly string PlotKeyword = "Plot Keyword";
        public static readonly string Genre = "Genre";
        public static readonly string OfficialSite = "Official Site";
        public static readonly string CountryOfOriginFilterName = "country_of_origin";
        public static readonly string Language = "Language";
        public static readonly string PrimaryLanguageFilterName = "primary_language";

        public static readonly Regex StarsSummaryRegex = new Regex(Star + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex DirectorsSummaryRegex = new Regex(Director + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex WritersSummaryRegex = new Regex(Writer + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex CreatorsSummaryRegex = new Regex(Creator + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex PersonIDURLMatcher = new Regex(Regex.Escape(PersonsPath) + PersonIDPrefix + @"(\d+)", RegexOptions.IgnoreCase);
        public static readonly Regex MovieYearRegex = new Regex(@"(.+)\((\d{4})\)\s*");
        public static readonly Regex CharacterRegex = new Regex(CharactersPath + PersonIDPrefix + @"(\d+).*?");
        public static readonly Regex TaglinesSummaryRegex = new Regex(Tagline + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex PlotKeywordsSummaryRegex = new Regex(PlotKeyword + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex KeywordLinkRegex = new Regex(Regex.Escape(KeywordsPath) + @"(.+)\?", RegexOptions.IgnoreCase);
        public static readonly Regex GenresSummaryRegex = new Regex(Genre + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex GenreLinkRegex = new Regex("title\\?genres=(.+?)[&\"]", RegexOptions.IgnoreCase);
        public static readonly Regex OfficialSitesHeaderRegex = new Regex(OfficialSite + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex CountriesHeaderRegex = new Regex("Countr[y]?(ies)?:", RegexOptions.IgnoreCase);
        public static readonly Regex CountryOfOriginRegex = new Regex(CountryOfOriginFilterName + "=(.+?)[&\"]", RegexOptions.IgnoreCase);
        public static readonly Regex LanguagesHeaderRegex = new Regex(Language + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex PrimaryLanguageRegex = new Regex(PrimaryLanguageFilterName + "=(.+?)[&\"]", RegexOptions.IgnoreCase);
    }
}
