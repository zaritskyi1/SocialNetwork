import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  hasRoles(roles: Array<string>): boolean {
    let hasRole = false;
    const userRoles = this.decodedToken.role as Array<string>;

    if (!userRoles) {
      return false;
    }

    roles.forEach(role => {
      if (userRoles.includes(role)) {
        hasRole = true;
      }
    });

    return hasRole;
  }

  getCurrentUserId(): string {
    return this.decodedToken.nameid;
  }

  hasAdminRights(): boolean {
    return (this.decodedToken.role as Array<string>).includes('Administrator');
  }

  hasModeratorRights(): boolean {
    return (this.decodedToken.role as Array<string>).includes('Moderator');
  }

  hasAdminAccess(): boolean {
    return this.hasRoles(['Administrator', 'Moderator']);
  }
}
