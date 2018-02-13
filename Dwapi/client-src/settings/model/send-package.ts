import {CentralRegistry} from './central-registry';

export interface SendPackage {
    destination?: CentralRegistry;
    endpoint?: string;
}
