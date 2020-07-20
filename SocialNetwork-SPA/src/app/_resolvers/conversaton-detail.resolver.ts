import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { catchError, tap } from 'rxjs/operators';
import { of, Observable, pipe } from 'rxjs';
import { Conversation } from '../_models/conversation';
import { ConversationService } from '../_services/conversation.service';

@Injectable()
export class ConversationDetailResolver implements Resolve<Conversation> {
    constructor(private conversationService: ConversationService, private router: Router,
                private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Conversation> {
        return this.conversationService.getConversation(route.params.id).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/conversations']);
                return of(null);
            }),
            pipe(
                tap(conversation => {
                    if (!conversation.isUnread) {
                        this.conversationService.markConversationAsRead(conversation.id).subscribe();
                    }
                })
            )
        );
    }

}
