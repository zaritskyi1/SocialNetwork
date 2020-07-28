import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MessageForSent } from 'src/app/_models/message-for-sent';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-message-form',
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.css']
})
export class MessageFormComponent implements OnInit {
  @Input() conversationId: string;
  @Input() currentUserId: string;
  @Output() messageSent: EventEmitter<Message> = new EventEmitter<Message>();
  messageForSent: MessageForSent;

  constructor(
    private messageService: MessageService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.initMessageForSent();
  }

  sendMessage() {
    if (this.messageForSent.content.length > 0 && this.messageForSent.content.length <= 512) {
      this.messageService.sendMessage(this.messageForSent).subscribe((message: Message) => {
        this.messageSent.emit(message);
      }, error => {
        this.alertify.error(error);
      });
      this.messageForSent.content = '';
    } else {
      this.alertify.error('Message must contain at least 1 and maximum 512 characters.');
    }
  }

  private initMessageForSent() {
    this.messageForSent = {
      conversationId: this.conversationId,
      userId: this.currentUserId,
      content: ''
    };
  }

}
