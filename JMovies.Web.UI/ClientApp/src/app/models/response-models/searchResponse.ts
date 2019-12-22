import { ComplexCollection } from '../general-models/complexCollection';
import { SearchResult } from '../general-models/searchResult';

export class SearchResponse {
    searchResults: ComplexCollection<SearchResult>;
}
