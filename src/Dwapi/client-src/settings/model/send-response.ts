import {ExtractEvent} from './extract-event';

export interface SendResponse {
    isComplete?: boolean;
    isSending?: boolean;
    eEvent?: ExtractEvent;
}
