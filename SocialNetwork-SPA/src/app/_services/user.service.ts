import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserForList } from '../_models/user-for-list';
import { User } from '../_models/user';
import { FriendshipWithStatus } from '../_models/friendship-with-status';
import { Conversation } from '../_models/conversation';
import { PaginatedResult } from '../_models/paginated-result';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }

  getUsers(page?, itemsPerPage?): Observable<PaginatedResult<UserForList[]>> {
    const paginatedResult: PaginatedResult<UserForList[]> = new PaginatedResult<UserForList[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<UserForList[]>(this.baseUrl, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + id);
  }

  updateUser(id: string, user: User) {
    return this.http.put(this.baseUrl + id, user);
  }

  getFriendshipStatus(id: string): Observable<FriendshipWithStatus> {
    return this.http.get<FriendshipWithStatus>(this.baseUrl + id + '/friendship-status');
  }

  getConversation(id: string): Observable<Conversation> {
    return this.http.get<Conversation>(this.baseUrl + id + '/conversation');
  }

}
