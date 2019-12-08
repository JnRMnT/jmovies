import { ComplexCollection } from '../general-models/complexCollection';
import { Credit } from '../general-models/credit';

export class GetProductionCastResponse {
    cast: ComplexCollection<Credit>;
}
