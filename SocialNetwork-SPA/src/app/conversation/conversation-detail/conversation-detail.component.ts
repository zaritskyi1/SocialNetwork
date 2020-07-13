import { Component, OnInit } from '@angular/core';
import { ConversationService } from 'src/app/_services/conversation.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Conversation } from 'src/app/_models/conversation';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-conversation-detail',
  templateUrl: './conversation-detail.component.html',
  styleUrls: ['./conversation-detail.component.css']
})
export class ConversationDetailComponent implements OnInit {
  conversation: Conversation;

  constructor(private route: ActivatedRoute, private conversationService: ConversationService,
              private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.loadConversation();
  }

  loadConversation() {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.conversationService.getConversation(id).subscribe(
      result => {
        this.conversation = result;
        this.transformConversation();
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  transformConversation() {
    if (this.conversation.title === null) {
      if (this.conversation.participants[0].userId === this.authService.decodedToken.nameid) {
        this.conversation.title = this.conversation.participants[1].user.name + ' ' + this.conversation.participants[1].user.surname;
      } else {
        this.conversation.title = this.conversation.participants[0].user.name + ' ' + this.conversation.participants[0].user.surname;
      }
    }
  }
}
