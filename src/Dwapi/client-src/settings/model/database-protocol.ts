export interface DatabaseProtocol {
    id?: string;
    databaseType?: number;
    databaseTypeName?: string;
    host?: string;
    port?: number;
    username?: string;
    password?: string;
    databaseName?: string;
    advancedProperties?: string;
    emrSystemId?: string;
}
