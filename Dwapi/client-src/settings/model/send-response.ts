import {ExtractEvent} from './extract-event';

export interface SendResponse {
    IsComplete?: boolean;
    IsSending?: boolean;
    eEvent?: ExtractEvent;
}
