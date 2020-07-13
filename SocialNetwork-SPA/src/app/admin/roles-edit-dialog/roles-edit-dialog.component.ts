import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-roles-edit-dialog',
  templateUrl: './roles-edit-dialog.component.html',
  styleUrls: ['./roles-edit-dialog.component.css']
})
export class RolesEditDialogComponent {
  @Output() changedRoles = new EventEmitter();
  roles = [
    {
      roleName: 'User',
      isChecked: false
    },
    {
      roleName: 'Moderator',
      isChecked: false
    },
    {
      roleName: 'Administrator',
      isChecked: false
    },
  ];

  constructor(public dialogRef: MatDialogRef<RolesEditDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any)
  {
    this.roles.forEach(role => {
      if ((data.roles as Array<string>).includes(role.roleName)) {
        role.isChecked = true;
      }
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateRoles() {
    this.changedRoles.emit(this.roles);
    this.dialogRef.close();
  }
}
