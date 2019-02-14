import {Resource} from './resource';

export interface RestProtocol {
    id?: string;
    url?: string;
    authToken?: string;
    emrSystemId?: string;
    resources?: Resource[];
}
