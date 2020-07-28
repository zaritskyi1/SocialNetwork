import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Pagination } from 'src/app/_models/pagination';
import { PaginatedResult } from 'src/app/_models/paginated-result';
import { UserForList } from 'src/app/_models/user-for-list';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: UserForList[];
  pagination: Pagination;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users.result;
      this.pagination = data.users.pagination;
    });
  }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((result: PaginatedResult<UserForList[]>) => {
        this.users = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

}
