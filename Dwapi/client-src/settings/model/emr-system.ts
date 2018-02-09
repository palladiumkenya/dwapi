import {DatabaseProtocol} from './database-protocol';
import {RestProtocol} from './rest-protocol';

export interface EmrSystem {
    id?: string;
    name?: string;
    version?: string;
    databaseProtocols?: DatabaseProtocol[];
    restProtocols?: RestProtocol[];
}
