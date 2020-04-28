export interface ExtractEvent {
    lastStatus?: string;
    found?: number;
    loaded?: number;
    excluded?:number;
    rejected?: number;
    queued?: number;
    sent?: number;
}
