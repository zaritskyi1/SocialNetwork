import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  hasAdminRights: boolean;
  hasModeratorRights: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.hasAdminRights = this.authService.hasAdminRights();
    this.hasModeratorRights = this.authService.hasModeratorRights();
  }

}
