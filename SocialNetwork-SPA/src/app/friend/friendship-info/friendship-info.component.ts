import { Component, OnInit } from '@angular/core';
import { FriendshipWithUser } from 'src/app/_models/friendship-with-user';
import { Pagination } from 'src/app/_models/pagination';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-friendship-info',
  templateUrl: './friendship-info.component.html',
  styleUrls: ['./friendship-info.component.css']
})
export class FriendshipInfoComponent implements OnInit {
  friendships: FriendshipWithUser[];
  pagination: Pagination;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.friendships = data.friendships.result;
      this.pagination = data.friendships.pagination;
    });
  }

}
