import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CharacterListModel } from '@app/models/character-list-model';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  baseUrl = `${environment.baseUrl}/character`;

  constructor(private readonly http: HttpClient) { }

  getCharacterList(sort?: string, direction?: number) : Observable<CharacterListModel[]> {
    let params = new HttpParams();

    if (sort) params = params.append('sort', sort);
    if (direction != null) params = params.append('direction', direction.toString());

    return this.http.get<CharacterListModel[]>(this.baseUrl + '/list', { params });
  }
}
