export interface UserForList {
    id: string;
    userName: string;
    name: string;
    surname: string;
    city: string;
    country: string;
    lastActive: Date;
    roles?: Array<string>;
}
