import { CentralRegistry } from './central-registry';
import { EmrSetup } from './emr-setup';
import { Extract } from './extract';

export interface UploadPackage {
    destination?: CentralRegistry;
    docket?: string;
    extractId?: string;
    extractName?: string;
    endpoint?: string;
    emrSetup?: EmrSetup;
    emrId?: string;
    emrName?: string;
    extracts?: Extract[];
    file?: Blob;  
}
