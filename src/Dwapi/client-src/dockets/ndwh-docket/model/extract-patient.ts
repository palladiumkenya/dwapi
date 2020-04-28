import {DatabaseProtocol} from '../../../settings/model/database-protocol';
import {Extract} from '../../../settings/model/extract';

export interface ExtractPatient {
    extract?: Extract;
    databaseProtocol?: DatabaseProtocol;
}
