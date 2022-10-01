import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CharacterListModel } from '@app/models/character';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  baseUrl = `${environment.baseUrl}/character`;

  constructor(private readonly http: HttpClient) { }

  getCharacterList() : Observable<CharacterListModel[]> {
    return this.http.get<CharacterListModel[]>(this.baseUrl + '/list');
  }
}
