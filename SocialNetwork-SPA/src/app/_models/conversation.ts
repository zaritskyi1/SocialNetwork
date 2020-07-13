import { Participant } from './participant';

export class Conversation {
    id: string;
    title: string;
    lastMessageDate: Date;
    createdDate: Date;
    participants: Participant[];
}
