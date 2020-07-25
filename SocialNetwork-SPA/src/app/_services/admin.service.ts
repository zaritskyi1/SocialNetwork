import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserForList } from '../_models/userForList';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl + 'admin/';

  constructor(private http: HttpClient) { }

  getUsersWithRoles(pageNumber?, pageSize?): Observable<PaginatedResult<UserForList[]>> {
    const paginatedResult: PaginatedResult<UserForList[]> = new PaginatedResult<UserForList[]>();

    let params = new HttpParams();

    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }

    return this.http.get<UserForList[]>(this.baseUrl + 'users', { observe: 'response', params })
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

  changeUserRole(id: string, roles: string[]) {
    return this.http.put(this.baseUrl + 'users/' + id + '/role', { roleNames: roles});
  }
}
