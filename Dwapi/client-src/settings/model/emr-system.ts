import {DatabaseProtocol} from './database-protocol';
import {RestProtocol} from './rest-protocol';

export interface EmrSystem {
    id?: string;
    name?: string;
    version?: string;
    isMiddleware?: boolean;
    isDefault?: boolean;
    databaseProtocols?: DatabaseProtocol[];
    restProtocols?: RestProtocol[];
}
