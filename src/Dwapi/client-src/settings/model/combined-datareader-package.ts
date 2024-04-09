import { SendPackage } from './send-package';
import {EmrSetup} from "./emr-setup";

export interface CombinedSmartReaderPackage {
    dwhPackage?: SendPackage;
    mpiPackage?: SendPackage;
    sendMpi?: boolean;
    jobId?:string;
    dbProtocol?:string;

}
