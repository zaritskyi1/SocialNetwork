import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { FriendshipWithUser } from '../_models/friendship-with-user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { FriendshipRequest } from '../_models/friendship-request';
import { FriendshipWithStatus } from '../_models/friendship-with-status';

@Injectable({
  providedIn: 'root'
})
export class FriendService {
  baseUrl = environment.apiUrl + 'friends/';

  constructor(private http: HttpClient) { }

  getFriends(page?, itemsPerPage?): Observable<PaginatedResult<FriendshipWithUser[]>> {
    const paginatedResult: PaginatedResult<FriendshipWithUser[]> = new PaginatedResult<FriendshipWithUser[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<FriendshipWithUser[]>(this.baseUrl, { observe: 'response', params })
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

  getFriendsRequests(page?, itemsPerPage?): Observable<PaginatedResult<FriendshipWithUser[]>> {
    const paginatedResult: PaginatedResult<FriendshipWithUser[]> = new PaginatedResult<FriendshipWithUser[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<FriendshipWithUser[]>(this.baseUrl + 'requests', { observe: 'response', params })
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

  acceptFriendRequest(id: string) {
    return this.http.put(this.baseUrl + id + '/accept', null);
  }

  createFriendRequest(friendRequest: FriendshipRequest): Observable<FriendshipWithStatus> {
    return this.http.post<FriendshipWithStatus>(this.baseUrl, friendRequest);
  }

  removeFriendship(id: string) {
    return this.http.delete(this.baseUrl + id);
  }
}
