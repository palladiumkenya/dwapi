import {LoadFromEmrCommand} from './load-from-emr-command';
import {ExtractPatient} from '../../dockets/ndwh-docket/model/extract-patient';
import {LoadPrepFromEmrCommand} from './load-prep-from-emr-command';

export interface LoadPrepExtracts {
    loadPrepFromEmrCommand?: LoadPrepFromEmrCommand;
}
