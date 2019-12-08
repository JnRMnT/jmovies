import { JMResultItem } from './jm-result-item';
import { ComplexCollection } from '../complexCollection';
import { RedirectionInfo } from './redirection-info';

export class JMResult {
    errors: ComplexCollection<JMResultItem>;
    warnings: ComplexCollection<JMResultItem>;
    informations: ComplexCollection<JMResultItem>;
    redirectionInfo: RedirectionInfo;
    isSuccess: boolean;
    hasWarning: boolean;
    hasInformation: boolean;
}
