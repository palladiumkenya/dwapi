import {ExtractEvent} from './extract-event';
import {SendEvent} from './send-event';

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
    sendEvent?: SendEvent;
    databaseProtocolId?: string;
}
