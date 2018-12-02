import { ProductionTypeEnum } from "./production-type-enum";
import { Credit } from "./credit";
import { ActingCredit } from "./acting-credit";

export class Movie {
    imdbLink: string;
    imDbID: number;
    title: string;
    originalTitle: string;
    plotSummary: string;
    storyLine: string;
    credits: Credit | ActingCredit[];
    year: number;
    tagLines: string[];

    //public Keyword[] Keywords { get; set; }
    //public Genre[] Genres { get; set; }
    //public OfficialSite[] OfficialSites { get; set; }
    //public Country[] Countries { get; set; }
    //public Language[] Languages { get; set; }
    //public ReleaseDate[] ReleaseDates { get; set; }
    //public AKA[] AKAs { get; set; }
    filmingLocations: string[];
    //public Budget Budget { get; set; }
    //public Company[] ProductionCompanies { get; set; }
    //public TimeSpan Runtime { get; set; }
    productonType: ProductionTypeEnum;
}