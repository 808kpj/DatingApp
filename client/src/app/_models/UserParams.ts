import { User } from "./user";

export class UserParams {
    gender:string;
    minAge = 18;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 5;
    orderBy = 'lastActive';

    ///will set the gender filter to the opposite sex based off the user when they login
    constructor(user: User | null) {
        this.gender = user?.gender === 'female' ? 'male' : 'female'
    }
}
