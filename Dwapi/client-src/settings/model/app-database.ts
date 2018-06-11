import {DatabaseProvider} from './database-provider';

export interface AppDatabase {
    provider?: DatabaseProvider;
    server?: string;
    port?: number;
    database?: string;
    user?: string;
    password?: string;
}
