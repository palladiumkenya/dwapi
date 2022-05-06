import {LoadFromEmrCommand} from './load-from-emr-command';
import {ExtractPatient} from '../../dockets/ndwh-docket/model/extract-patient';
import {EmrSetup} from "./emr-setup";

export interface LoadExtracts {
    loadFromEmrCommand?: LoadFromEmrCommand;
    extractMpi?: ExtractPatient;
    loadMpi?: boolean;
    emrSetup?: EmrSetup;
}

