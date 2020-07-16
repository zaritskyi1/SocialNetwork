import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FriendService } from 'src/app/_services/friend.service';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { FriendshipWithUser } from 'src/app/_models/friendship-with-user';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-friendship-request-list',
  templateUrl: './friendship-request-list.component.html',
  styleUrls: ['./friendship-request-list.component.css']
})
export class FriendshipRequestListComponent implements OnInit {
  friendships: FriendshipWithUser[];
  pagination: Pagination;

  constructor(private friendService: FriendService,  private alertify: AlertifyService) { }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadFriendRequestList();
  }

  ngOnInit() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.loadFriendRequestList();
  }

  loadFriendRequestList() {
    this.friendService.getFriendsRequests(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((result: PaginatedResult<FriendshipWithUser[]>) => {
      this.friendships = result.result;
      this.pagination = result.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  acceptFriendRequest(id: string) {
    this.friendService.acceptFriendRequest(id).subscribe(
      next => {
        this.alertify.success('Friend request accepted!');
        this.loadFriendRequestList();
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  declineFriendRequest(id: string) {
    this.friendService.removeFriendship(id).subscribe(
      next => {
        this.alertify.success('Friend request declined!');
        this.loadFriendRequestList();
      }, error => {
        this.alertify.error(error);
      }
    );
  }
}
