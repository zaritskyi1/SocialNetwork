import { UserForList } from './user-for-list';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';

export interface MessageReport {
    id: string;
    createdDate: Date;
    userId: string;
    conversationId: string;
    user?: UserForList;
    message?: Message;
}
