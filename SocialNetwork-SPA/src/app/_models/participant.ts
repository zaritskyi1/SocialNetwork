import { UserForList } from './user-for-list';

export interface Participant {
    hasUnreadMessages: boolean;
    userId: string;
    user: UserForList;
}
