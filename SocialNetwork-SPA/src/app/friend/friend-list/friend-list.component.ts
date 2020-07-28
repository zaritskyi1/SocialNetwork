import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { FriendshipWithUser } from '../../_models/friendship-with-user';
import { Pagination } from '../../_models/pagination';
import { PaginatedResult } from '../../_models/paginated-result';
import { PageEvent } from '@angular/material/paginator';
import { FriendService } from '../../_services/friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  @Input() friendships: FriendshipWithUser[];
  @Input() pagination: Pagination;

  constructor(
    private friendService: FriendService,
    private alertify: AlertifyService
  ) { }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadFriendList();
  }

  ngOnInit() {
  }

  loadFriendList() {
    this.friendService.getFriends(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((result: PaginatedResult<FriendshipWithUser[]>) => {
        this.friendships = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  removeFriend(id: string) {
    this.friendService.removeFriendship(id).subscribe(
      next => {
        this.loadFriendList();
        this.alertify.success('Friend removed!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

}
