import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService, private router: Router,
    private alertify: AlertifyService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const roles = route.firstChild.data.roles as Array<string>;

    if (roles) {
      const permission = this.authService.hasRoles(roles);
      if (permission) {
        return true;
      } else {
        this.router.navigate(['friends']);
        this.alertify.error('You don`t have permission to access this resource!');
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('You are not authorized!');
    this.router.navigate(['/login']);
    return false;
  }
}
