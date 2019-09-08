import { Credit } from "./credit";
import { ActingCredit } from "./acting-credit";
import { BaseProduction } from './production';
import { TagLine } from './tag-line';
import { ComplexCollection } from './complexCollection';

export class Movie extends BaseProduction {
  originalTitle: string;
  plotSummary: string;
  storyLine: string;
  credits: ComplexCollection<Credit| ActingCredit>;
  tagLines: TagLine[];

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
}
