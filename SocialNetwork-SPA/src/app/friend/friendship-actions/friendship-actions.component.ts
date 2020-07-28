import { Component, OnInit, Input } from '@angular/core';
import { FriendService } from 'src/app/_services/friend.service';
import { FriendshipWithStatus } from 'src/app/_models/friendship-with-status';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FriendshipRequest } from 'src/app/_models/friendship-request';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-friendship-actions',
  templateUrl: './friendship-actions.component.html',
  styleUrls: ['./friendship-actions.component.css']
})
export class FriendshipActionsComponent implements OnInit {
  friendshipWithStatus: FriendshipWithStatus;
  @Input() userId: string;
  @Input() currentUserId: string;

  constructor(
    private friendService: FriendService,
    private alertify: AlertifyService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.loadFriendship();
  }

  loadFriendship() {
    this.userService.getFriendshipStatus(this.userId).subscribe(
      result => {
        this.friendshipWithStatus = result;
      }, error => {
        this.friendshipWithStatus = null;
      }
    );
  }

  acceptFriendRequest(id: string) {
    this.friendService.acceptFriendRequest(id).subscribe(
      next => {
        this.friendshipWithStatus.status = 'Accepted';
        this.alertify.success('Friend request accepted!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  declineFriendRequest(id: string) {
    this.friendService.removeFriendship(id).subscribe(
      next => {
        this.friendshipWithStatus = null;
        this.alertify.success('Friend request declined!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  removeFriend(id: string) {
    this.friendService.removeFriendship(id).subscribe(
      next => {
        this.friendshipWithStatus = null;
        this.alertify.success('Friend successfully removed!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  addFriend(id: string) {
    const friendshipRequest: FriendshipRequest = {
      senderId: this.currentUserId,
      receiverId: id
    };

    this.friendService.createFriendRequest(friendshipRequest).subscribe(
      next => {
        this.friendshipWithStatus = next;
        this.alertify.success('Request successfully sent!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

}
