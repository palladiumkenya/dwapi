import {DatabaseProtocol} from './database-protocol';
import { DwhExtract } from './dwh-extract';

export interface LoadFromEmrCommand {
    extracts?: DwhExtract[];
    databaseProtocol?: DatabaseProtocol;
}
