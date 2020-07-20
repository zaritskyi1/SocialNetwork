import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Conversation } from 'src/app/_models/conversation';
import { Message } from 'src/app/_models/message';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { MessageService } from 'src/app/_services/message.service';
import { AuthService } from 'src/app/_services/auth.service';
import { ReportService } from 'src/app/_services/report.service';
import { ConversationService } from 'src/app/_services/conversation.service';
import { PageEvent } from '@angular/material/paginator';
import { MessageForSent } from 'src/app/_models/messageForSent';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent implements OnInit {
  @Input() conversation: Conversation;
  messages: Message[];
  pagination: Pagination;
  currentUserId: string;
  messageForSent: MessageForSent;

  constructor(private messageService: MessageService, private alertify: AlertifyService,
              private authService: AuthService, private reportService: ReportService,
              private conversationService: ConversationService) { }

  ngOnInit() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.loadMessages();
    this.currentUserId = this.authService.decodedToken.nameid;
    this.messageForSent = {
      conversationId: this.conversation.id,
      userId: this.currentUserId,
      content: ''
    };
  }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadMessages();
  }

  loadMessages() {
    this.conversationService.getConversationMessages(this.conversation.id, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((result: PaginatedResult<Message[]>) => {
      this.messages = result.result.reverse();
      this.pagination = result.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  reportOnMessage(id: string) {
    this.reportService.sendMessageReport(id).subscribe(
      next => {
        this.alertify.success('Reported successfully');
      }, error => [
        this.alertify.error(error)
      ]
    );
  }

  sendMessage() {
    this.messageService.sendMessage(this.messageForSent).subscribe((message: Message) => {
      this.messages.push(message);
    }, error => {
      console.log(error);
    });
    this.messageForSent.content = '';
  }
}
