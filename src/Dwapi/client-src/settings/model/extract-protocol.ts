import {Extract} from './extract';
import {DatabaseProtocol} from './database-protocol';

export interface ExtractDatabaseProtocol {
    extract?: Extract;
    databaseProtocol?: DatabaseProtocol;
}
