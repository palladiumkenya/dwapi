import { ExtractEvent } from './extract-event';

export interface DwhExtract {
    id?: string;
    extractName?: string;
    name?: string;
    display?: string;
    sqlquery?: string;
    extractSql?: string;
    destination?: string;
    rank?: string;
    isPriority?: string;
    docketId?: string;
    emrSystemId?: string;
    emr?: string;
    extractEvent?: ExtractEvent;
}
