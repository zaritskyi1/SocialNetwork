import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { FriendshipWithUser } from '../_models/friendship-with-user';
import { FriendService } from '../_services/friend.service';

@Injectable()
export class FriendListResolver implements Resolve<FriendshipWithUser[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private friendshipService: FriendService, private router: Router,
                private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<FriendshipWithUser[]> {
        return this.friendshipService.getFriends(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }

}
