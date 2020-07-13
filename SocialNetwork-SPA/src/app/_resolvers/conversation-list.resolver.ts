import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Conversation } from '../_models/conversation';
import { Observable, of } from 'rxjs';
import { ConversationService } from '../_services/conversation.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ConversationListResolver implements Resolve<Conversation[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private conversationService: ConversationService, private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Conversation[]> {
        return this.conversationService.getConversations(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                console.log(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
