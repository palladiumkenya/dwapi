import {ExtractEvent} from './extract-event';

export interface Extract {
    id?: string;
    name?: string;
    display?: string;
    extractSql?: string;
    destination?: string;
    rank?: string;
    isPriority?: string;
    docketId?: string;
    emrSystemId?: string;
    emr?: string;
    extractEvent?: ExtractEvent;
    databaseProtocolId?: string;
}
