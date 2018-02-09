export interface DatabaseProtocol {
    id?: string;
    databaseType?: number;
    host?: string;
    Port?: number;
    username?: string;
    password?: string;
    databaseName?: string;
    advancedProperties?: string;
    emrSystemId?: string;
}
