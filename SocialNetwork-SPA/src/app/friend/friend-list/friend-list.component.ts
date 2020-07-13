import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { Friendship } from '../../_models/friendship';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { PageEvent } from '@angular/material/paginator';
import { FriendService } from '../../_services/friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  friendships: Friendship[];
  pagination: Pagination;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private friendService: FriendService) { }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadFriendList();
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.friendships = data.friendships.result;
      this.pagination = data.friendships.pagination;
    });
  }

  loadFriendList() {
    this.friendService.getFriends(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((result: PaginatedResult<Friendship[]>) => {
      this.friendships = result.result;
      this.pagination = result.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }
}
