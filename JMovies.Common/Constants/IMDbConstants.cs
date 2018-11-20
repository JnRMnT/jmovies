using System;
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

        public static readonly string Star = "Star";
        public static readonly string Director = "Director";
        public static readonly string Writer = "Writer";
        public static readonly string Creator = "Creator";
        public static readonly Regex StarsSummaryRegex = new Regex(Star + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex DirectorsSummaryRegex = new Regex(Director + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex WritersSummaryRegex = new Regex(Writer + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex CreatorsSummaryRegex = new Regex(Creator + "[s]?:", RegexOptions.IgnoreCase);
        public static readonly Regex PersonIDURLMatcher = new Regex(Regex.Escape(PersonsPath) + PersonIDPrefix + @"(\d+)", RegexOptions.IgnoreCase);
        public static readonly Regex MovieYearRegex = new Regex(@"(.+)\((\d{4})\)\s*");
        public static readonly Regex CharacterRegex = new Regex(CharactersPath + PersonIDPrefix + @"(\d+).*?");
    }
}
