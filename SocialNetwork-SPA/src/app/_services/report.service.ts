import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AlertifyService } from './alertify.service';
import { map } from 'rxjs/operators';
import { MessageReport } from '../_models/message-report';
import { PaginatedResult } from '../_models/paginated-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  baseUrl = environment.apiUrl + 'report/';

  constructor(private http: HttpClient) { }

  getReportedMessages(pageNumber?, pageSize?): Observable<PaginatedResult<MessageReport[]>> {
    const paginatedResult: PaginatedResult<MessageReport[]> = new PaginatedResult<MessageReport[]>();

    let params = new HttpParams();

    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }

    return this.http.get<MessageReport[]>(this.baseUrl + 'messages', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  sendMessageReport(id: string) {
    return this.http.post(this.baseUrl + 'messages/' + id, null);
  }

  deleteMessagedreport(id: string) {
    return this.http.delete(this.baseUrl + 'messages/' + id + '/decline');
  }

  acceptMessagedreport(id: string) {
    return this.http.delete(this.baseUrl + 'messages/' + id + '/accept');
  }

}
