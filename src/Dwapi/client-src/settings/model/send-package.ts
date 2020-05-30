import {CentralRegistry} from './central-registry';
import {EmrSetup} from './emr-setup';

export interface SendPackage {
    destination?: CentralRegistry;
    docket?: string;
    extractId?: string;
    extractName?: string;
    endpoint?: string;
    emrSetup?: EmrSetup;
    emrId?: string;
    emrName?: string;
}
