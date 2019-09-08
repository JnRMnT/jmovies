import { Credit } from "./credit";
import { Character } from "./character";
import { ComplexCollection } from './complexCollection';

export class ActingCredit extends Credit {
    characters: ComplexCollection<Character>;
}
