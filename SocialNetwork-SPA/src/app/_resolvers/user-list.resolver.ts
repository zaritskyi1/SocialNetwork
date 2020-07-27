import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { FriendshipWithUser } from '../_models/friendship-with-user';
import { FriendService } from '../_services/friend.service';
import { UserForList } from '../_models/userForList';
import { UserService } from '../_services/user.service';

@Injectable()
export class UserListResolver implements Resolve<UserForList[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private userService: UserService, private router: Router,
                private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<UserForList[]> {
        return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }

}
