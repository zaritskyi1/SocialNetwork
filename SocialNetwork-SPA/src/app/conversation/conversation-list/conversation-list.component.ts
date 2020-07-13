import { Component, OnInit } from '@angular/core';
import { Conversation } from 'src/app/_models/conversation';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { PageEvent } from '@angular/material/paginator';
import { AuthService } from 'src/app/_services/auth.service';
import { ConversationService } from 'src/app/_services/conversation.service';

@Component({
  selector: 'app-conversation-list',
  templateUrl: './conversation-list.component.html',
  styleUrls: ['./conversation-list.component.css']
})
export class ConversationListComponent implements OnInit {
  conversations: Conversation[];
  pagination: Pagination;

  constructor(private route: ActivatedRoute, private conversationService: ConversationService,
              private alertify: AlertifyService, private authService: AuthService) { }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadConversations();
    this.transformConversations();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.conversations = data.conversations.result;
      this.pagination = data.conversations.pagination;
      this.transformConversations();
    });
  }

  loadConversations() {
    this.conversationService.getConversations(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((result: PaginatedResult<Conversation[]>) => {
      this.conversations = result.result;
      this.pagination = result.pagination;
      this.transformConversations();
    }, error => {
      this.alertify.error(error);
    });
  }

  transformConversations() {
    this.conversations.forEach(conversation => {
      if (conversation.title === null) {
        if (conversation.participants[0].userId === this.authService.decodedToken.nameid) {
          conversation.title = conversation.participants[1].user.name + ' ' + conversation.participants[1].user.surname;
        } else {
          conversation.title = conversation.participants[0].user.name + ' ' + conversation.participants[0].user.surname;
        }
      }
    });
  }
}
