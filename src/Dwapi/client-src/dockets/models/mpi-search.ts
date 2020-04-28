import { Gender } from './gender';

export interface MpiSearch {
    firstName?: string;
    middleName?: string;
    lastName?: number;
    dob?: Date;
    gender?: Gender;
    county?: string;
    nationalId?: string;
    phoneNumber?: string;
}
