import {DatabaseProtocol} from './database-protocol';
import {RestProtocol} from './rest-protocol';
import {Extract} from './extract';
import {EmrSetup} from "./emr-setup";

export interface EmrSystem {
    id?: string;
    name?: string;
    version?: string;
    isMiddleware?: boolean;
    isDefault?: boolean;
    databaseProtocols?: DatabaseProtocol[];
    restProtocols?: RestProtocol[];
    extracts?: Extract[];
    emrSetup?: EmrSetup;
}
