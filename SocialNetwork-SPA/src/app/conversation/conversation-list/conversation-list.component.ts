import { Component, OnInit } from '@angular/core';
import { Conversation } from 'src/app/_models/conversation';
import { Pagination } from 'src/app/_models/pagination';
import { PaginatedResult } from 'src/app/_models/paginated-result';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { PageEvent } from '@angular/material/paginator';
import { ConversationService } from 'src/app/_services/conversation.service';

@Component({
  selector: 'app-conversation-list',
  templateUrl: './conversation-list.component.html',
  styleUrls: ['./conversation-list.component.css']
})
export class ConversationListComponent implements OnInit {
  conversations: Conversation[];
  pagination: Pagination;

  constructor(
    private route: ActivatedRoute,
    private conversationService: ConversationService,
    private alertify: AlertifyService
  ) { }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadConversations();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.conversations = data.conversations.result;
      this.pagination = data.conversations.pagination;
    });
  }

  loadConversations() {
    this.conversationService.getConversations(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((result: PaginatedResult<Conversation[]>) => {
        this.conversations = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

}
