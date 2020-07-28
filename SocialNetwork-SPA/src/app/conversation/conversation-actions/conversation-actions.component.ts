import { Component, OnInit, Input } from '@angular/core';
import { Conversation } from 'src/app/_models/conversation';
import { UserService } from 'src/app/_services/user.service';
import { ConversationService } from 'src/app/_services/conversation.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ConversationForCreate } from 'src/app/_models/conversation-for-create';

@Component({
  selector: 'app-conversation-actions',
  templateUrl: './conversation-actions.component.html',
  styleUrls: ['./conversation-actions.component.css']
})
export class ConversationActionsComponent implements OnInit {
  conversation: Conversation;
  @Input() userId: string;
  @Input() currentUserId: string;

  constructor(
    private userService: UserService,
    private conversationService: ConversationService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.loadConversation();
  }

  loadConversation() {
    this.userService.getConversation(this.userId).subscribe(
      result => {
        this.conversation = result;
      }, error => {
        this.conversation = null;
      }
    );
  }

  createConversation() {
    const conversation: ConversationForCreate = {
      firstUserId: this.currentUserId,
      secondUserId: this.userId
    };

    this.conversationService.createConversation(conversation).subscribe(
      result => {
        this.conversation = result;
      }, error => {
        this.alertify.error(error);
      }
    );
  }

}
