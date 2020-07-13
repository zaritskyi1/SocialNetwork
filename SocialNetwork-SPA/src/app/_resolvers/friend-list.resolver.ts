import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Friendship } from '../_models/friendship';
import { FriendService } from '../_services/friend.service';

@Injectable()
export class FriendListResolver implements Resolve<Friendship[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private friendshipService: FriendService, private router: Router,
                private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Friendship[]> {
        return this.friendshipService.getFriends(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }

}
