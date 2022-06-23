import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LineChartModel } from '@app/models/line-chart-model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LineChartService {
  baseUrl = `${environment.baseUrl}/linechartdata/`;

  constructor(private readonly http: HttpClient) { }

  getData(username: string, dateFrom: Date, dateTo: Date, includeOverall: boolean): Observable<LineChartModel[]> {
    var params = new HttpParams();
    params = params.set('dateFrom', dateFrom.toISOString().slice(0, 10));
    params = params.set('dateTo', dateTo.toISOString().slice(0, 10));
    params = params.set('includeOverall', includeOverall);

    return this.http.get<any[]>(this.baseUrl + username, { params: params });
  }
}
