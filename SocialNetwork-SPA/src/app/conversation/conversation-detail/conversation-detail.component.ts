import { Component, OnInit } from '@angular/core';
import { ConversationService } from 'src/app/_services/conversation.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Conversation } from 'src/app/_models/conversation';
import { ActivatedRoute } from '@angular/router';
import { Participant } from 'src/app/_models/participant';

@Component({
  selector: 'app-conversation-detail',
  templateUrl: './conversation-detail.component.html',
  styleUrls: ['./conversation-detail.component.css']
})
export class ConversationDetailComponent implements OnInit {
  conversation: Conversation;
  participants: Participant[];

  constructor(private route: ActivatedRoute, private conversationService: ConversationService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(
      data => {
        this.conversation = data.conversation;
      }
    );
    this.loadParticipants();
  }

  loadParticipants() {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.conversationService.getConversationParticipants(id).subscribe(
      result => {
        this.participants = result.result;
      }, error => {
        this.alertify.error(error);
      }
    );
  }
}
