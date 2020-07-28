import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MessageForSent } from '../_models/message-for-sent';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl + 'messages';

  constructor(private http: HttpClient) { }

  sendMessage(message: MessageForSent) {
    return this.http.post(this.baseUrl, message);
  }

}
