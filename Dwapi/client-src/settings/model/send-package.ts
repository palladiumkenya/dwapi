import {CentralRegistry} from './central-registry';

export interface SendPackage {
    destination?: CentralRegistry;
    docket?: string;
    endpoint?: string;
}
