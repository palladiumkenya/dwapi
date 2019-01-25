import { SendPackage } from './send-package';

export interface CombinedPackage {
    dwhPackage?: SendPackage;
    mpiPackage?: SendPackage;
    sendMpi?: boolean;
}
