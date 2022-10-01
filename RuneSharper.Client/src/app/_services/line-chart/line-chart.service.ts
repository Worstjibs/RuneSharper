import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LineChartModel } from '@app/models/line-chart-model';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

interface QueryParams {
  dateFrom: Date;
  dateTo: Date;
  name: string;  
}

@Injectable({
  providedIn: 'root'
})
export class LineChartService {
  baseUrl = `${environment.baseUrl}/linechartdata/`;

  dataCache: Map<string, LineChartModel[]> = new Map();

  constructor(private readonly http: HttpClient) { }

  getData(username: string, dateFrom: Date, dateTo: Date, includeOverall: boolean): Observable<LineChartModel[]> {
    var params = new HttpParams();
    params = params.set('dateFrom', dateFrom.toISOString());
    params = params.set('dateTo', dateTo.toISOString());
    params = params.set('includeOverall', includeOverall);

    const key = this.toDelimitedString(dateFrom, dateTo, username);

    if (this.dataCache.has(key)) {
      return of(this.dataCache.get(key)!);
    }

    return this.http.get<LineChartModel[]>(this.baseUrl + username, { params: params })
      .pipe(
        map(model => {
          this.dataCache.set(key, model);
          return model;
        })
      );
  }

  private toDelimitedString(dateFrom: Date, dateTo: Date, username: string) {
    return `(${dateFrom.toISOString()})-(${dateTo.toISOString()})-(${username})`;
  }
}
