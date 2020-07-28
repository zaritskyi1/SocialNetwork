import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/_services/report.service';
import { MessageReport } from 'src/app/_models/message-report';
import { Pagination } from 'src/app/_models/pagination';
import { PageEvent } from '@angular/material/paginator';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MatDialog } from '@angular/material/dialog';
import { Message } from 'src/app/_models/message';
import { MessageDetailDialogComponent } from '../message-detail-dialog/message-detail-dialog.component';

@Component({
  selector: 'app-message-management',
  templateUrl: './message-management.component.html',
  styleUrls: ['./message-management.component.css']
})
export class MessageManagementComponent implements OnInit {
  messageReports: MessageReport[];
  pagination: Pagination;
  displayedColumns = ['createdDate', 'sendBy', 'messageDetail', 'buttons'];

  constructor(
    private reportService: ReportService,
    private alertify: AlertifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.initPagination();
    this.loadMessageReports();
  }

  loadMessageReports() {
    this.reportService.getReportedMessages(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
      result => {
        this.messageReports = result.result;
        this.pagination = result.pagination;
      }
    );
  }

  onPageChange(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadMessageReports();
  }

  declineMessageReport(id: string) {
    this.reportService.deleteMessagedreport(id).subscribe(
      next => {
        this.alertify.success('Report declined!');
        this.loadMessageReports();
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  acceptMessageReport(id: string) {
    this.reportService.acceptMessagedreport(id).subscribe(
      next => {
        this.alertify.success('Report accepted!');
        this.loadMessageReports();
      }, error => {
        this.alertify.error(error);
      }
    );
  }

  openMessageDetail(message: Message) {
    this.dialog.open(MessageDetailDialogComponent, {
      data: message
    });
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
