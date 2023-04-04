import {ExtractProfile} from '../../dockets/ndwh-docket/model/extract-profile';

export interface LoadPrepFromEmrCommand {
    extracts?: ExtractProfile[];
    loadChangesOnly?: boolean;

}
