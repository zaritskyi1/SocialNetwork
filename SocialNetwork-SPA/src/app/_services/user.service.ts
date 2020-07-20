import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserForList } from '../_models/userForList';
import { User } from '../_models/user';
import { FriendshipWithStatus } from '../_models/friendship-with-status';
import { Conversation } from '../_models/conversation';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<UserForList[]> {
    return this.http.get<UserForList[]>(this.baseUrl);
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
