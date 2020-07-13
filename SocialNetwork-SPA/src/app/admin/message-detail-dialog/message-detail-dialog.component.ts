import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Message } from 'src/app/_models/message';

@Component({
  selector: 'app-message-detail-dialog',
  templateUrl: './message-detail-dialog.component.html',
  styleUrls: ['./message-detail-dialog.component.css']
})
export class MessageDetailDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<MessageDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public message: Message) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
