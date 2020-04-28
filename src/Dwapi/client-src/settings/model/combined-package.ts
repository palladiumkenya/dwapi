import { SendPackage } from './send-package';
import {EmrSetup} from "./emr-setup";

export interface CombinedPackage {
    dwhPackage?: SendPackage;
    mpiPackage?: SendPackage;
    sendMpi?: boolean;
}
