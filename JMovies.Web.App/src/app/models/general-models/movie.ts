import { ProductionTypeEnum } from "./production-type-enum";

export class Movie {
    imdbId: number;
    title: string;
    originalTitle: string;
    plotSummary: string;
    StoryLine: string;
    // public Credit[] Credits { get; set; }
    Year: number;
    //public string[] TagLines { get; set; }
    //public Keyword[] Keywords { get; set; }
    //public Genre[] Genres { get; set; }
    //public OfficialSite[] OfficialSites { get; set; }
    //public Country[] Countries { get; set; }
    //public Language[] Languages { get; set; }
    //public ReleaseDate[] ReleaseDates { get; set; }
    //public AKA[] AKAs { get; set; }
    FilmingLocations: string[];
    //public Budget Budget { get; set; }
    //public Company[] ProductionCompanies { get; set; }
    //public TimeSpan Runtime { get; set; }
    ProductonType: ProductionTypeEnum;
}