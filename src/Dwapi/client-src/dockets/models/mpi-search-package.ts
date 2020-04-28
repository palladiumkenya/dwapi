import { CentralRegistry } from '../../settings/model/central-registry';
import { MpiSearch } from './mpi-search';

export interface SearchPackage {
    destination?: CentralRegistry;
    mpiSearch?: MpiSearch;
    endpoint?: string;
}
