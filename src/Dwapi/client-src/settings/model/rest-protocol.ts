import {Resource} from './resource';

export interface RestProtocol {
    id?: string;
    url?: string;
    authToken?: string;
    userName?: string;
    password?: string;
    emrSystemId?: string;
    resources?: Resource[];
}
