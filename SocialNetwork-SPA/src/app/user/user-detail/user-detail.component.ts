import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { FriendshipWithStatus } from 'src/app/_models/friendship-with-status';
import { AuthService } from 'src/app/_services/auth.service';
import { FriendService } from 'src/app/_services/friend.service';
import { FriendshipRequest } from 'src/app/_models/friendship-request';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: User;
  friendshipWithStatus: FriendshipWithStatus;
  currentUserId: string;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private userService: UserService, private authService: AuthService,
              private friendService: FriendService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });

    this.currentUserId = this.authService.getCurrentUserId();
    this.userService.getFriendshipStatus(this.route.snapshot.params.id).subscribe(
      next => {
        this.friendshipWithStatus = next;
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
