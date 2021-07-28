import {LoadFromEmrCommand} from './load-from-emr-command';
import {ExtractPatient} from '../../dockets/ndwh-docket/model/extract-patient';
import {LoadMnchFromEmrCommand} from './load-mnch-from-emr-command';

export interface LoadMnchExtracts {
    loadMnchFromEmrCommand?: LoadMnchFromEmrCommand;
}
