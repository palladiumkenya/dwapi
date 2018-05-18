import { ExtractEvent } from './extract-event';

export interface DwhExtract {
    id?: string;
    extractName?: string;
    display?: string;
    sqlquery?: string;
    destination?: string;
    rank?: string;
    isPriority?: string;
    docketId?: string;
    emrSystemId?: string;
    emr?: string;
    extractEvent?: ExtractEvent;
}
