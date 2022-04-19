import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LineChartService {
  baseUrl = `${environment.baseUrl}/linechartdata/`;

  constructor(private readonly http: HttpClient) { }

  getData(username: string, dateFrom: Date, dateTo: Date): Observable<any[]> {
    var params = new HttpParams();
    params = params.set('dateFrom', dateFrom.toISOString().slice(0, 10));
    params = params.set('dateTo', dateTo.toISOString().slice(0, 10));

    return this.http.get<any[]>(this.baseUrl + username, { params: params });
  }
}
