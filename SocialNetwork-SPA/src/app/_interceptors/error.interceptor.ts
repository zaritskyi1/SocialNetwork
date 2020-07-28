import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error instanceof HttpErrorResponse) {
                    const serverError = error.error;

                    if (error.status === 401) {
                        this.authService.logout();
                        return throwError(error.statusText);
                    }

                    if (serverError) {
                        if (serverError.errors) {
                            let errorList = '';

                            for (const key in serverError.errors) {
                                if (serverError.errors[key]) {
                                    if (serverError.errors[key].description) {
                                        errorList += serverError.errors[key].description + '\n';
                                    } else {
                                        errorList += serverError.errors[key] + '\n';
                                    }
                                }
                            }
                            return throwError(errorList);
                        }

                        if (serverError.detail) {
                            return throwError(serverError.detail);
                        }
                    }

                    if (error.status === 403) {
                        const deniedAccessMessage = 'You do not have access to this resource or action.';
                        return throwError(deniedAccessMessage);
                    }

                    const defaultMessage = 'Unknown error.';
                    return throwError(defaultMessage);
                }
            })
        );
    }

}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
