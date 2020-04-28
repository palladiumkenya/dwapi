import {LoadFromEmrCommand} from './load-from-emr-command';
import {ExtractPatient} from '../../dockets/ndwh-docket/model/extract-patient';
import {LoadHtsFromEmrCommand} from './load-hts-from-emr-command';

export interface LoadHtsExtracts {
    loadHtsFromEmrCommand?: LoadHtsFromEmrCommand;
}
