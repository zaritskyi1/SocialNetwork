import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserForList } from 'src/app/_models/user-for-list';
import { Pagination } from 'src/app/_models/pagination';
import { PaginatedResult } from 'src/app/_models/paginated-result';
import { MatDialog } from '@angular/material/dialog';
import { RolesEditDialogComponent } from '../roles-edit-dialog/roles-edit-dialog.component';
import { PageEvent } from '@angular/material/paginator';
import { UserDetailDialogComponent } from '../user-detail-dialog/user-detail-dialog.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: UserForList[];
  pagination: Pagination;
  displayedColumns = ['userName', 'userDetail', 'roles', 'button'];

  constructor(
    private adminService: AdminService,
    private alertify: AlertifyService,
    public dialog: MatDialog
  ) { }

  openRolesEditDialog(user: UserForList) {
    const dialogRef = this.dialog.open(RolesEditDialogComponent, {
      data: user
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const roles = result
          .filter(role => role.isChecked)
          .map(r => r.roleName);

        this.adminService.changeUserRole(user.id, roles).subscribe(next => {
          this.alertify.success('Information updated successfully!');
          user.roles = roles;
        }, error => {
          this.alertify.error(error);
        });
      }
    }, error => {
      this.alertify.error(error);
    });
  }

  openUserDetailsDialog(user: UserForList) {
    this.dialog.open(UserDetailDialogComponent, { data: user });
  }

  ngOnInit() {
    this.initPagination();
    this.loadUsers();
  }

  loadUsers() {
    this.adminService.getUsersWithRoles(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
      (result: PaginatedResult<UserForList[]>) => {
        this.users = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadUsers();
  }

  initPagination() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 10,
      totalPages: 1,
    };
  }

}
