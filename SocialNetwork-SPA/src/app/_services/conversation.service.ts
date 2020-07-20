import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Conversation } from '../_models/conversation';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';
import { HttpParams, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { Message } from '../_models/message';
import { ConversationForCreate } from '../_models/conversation-for-create';
import { Participant } from '../_models/participant';

@Injectable({
  providedIn: 'root'
})
export class ConversationService {
  baseUrl = environment.apiUrl + 'conversations/';

  constructor(private http: HttpClient, private authService: AuthService) { }

  getConversations(page?, itemsPerPage?): Observable<PaginatedResult<Conversation[]>> {
    const paginatedResult: PaginatedResult<Conversation[]> = new PaginatedResult<Conversation[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Conversation[]>(this.baseUrl, { observe: 'response', params })
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

  getConversationMessages(id, page?, itemsPerPage?): Observable<PaginatedResult<Message[]>> {
    const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Message[]>(this.baseUrl + id + '/messages', { observe: 'response', params })
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

  getConversation(id: string): Observable<Conversation> {
    return this.http.get<Conversation>(this.baseUrl + id);
  }

  createConversation(conversation: ConversationForCreate) {
    return this.http.post<Conversation>(this.baseUrl, conversation);
  }

  markConversationAsRead(id: string) {
    return this.http.put(this.baseUrl + id + '/read', null);
  }

  getConversationParticipants(id: string, page?, itemsPerPage?): Observable<PaginatedResult<Participant[]>> {
    const paginatedResult: PaginatedResult<Participant[]> = new PaginatedResult<Participant[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Participant[]>(this.baseUrl + id + '/participants', { observe: 'response', params }).pipe(
      map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
    );
  }
}
