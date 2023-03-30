import {ExtractProfile} from '../../dockets/ndwh-docket/model/extract-profile';

export interface LoadMnchFromEmrCommand {
    extracts?: ExtractProfile[];
    loadChangesOnly?: boolean;

}
