import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CharacterList } from '@app/models/character-list.model';
import { CharacterView } from '@app/models/character-view.model';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  baseUrl = `${environment.baseUrl}/character`;

  constructor(private readonly http: HttpClient) { }

  getCharacterList(sort?: string, direction?: number) : Observable<CharacterList[]> {
    let params = new HttpParams();

    if (sort) params = params.append('sort', sort);
    if (direction != null) params = params.append('direction', direction.toString());

    return this.http.get<CharacterList[]>(this.baseUrl, { params });
  }

  getCharacterView(userName: string) : Observable<CharacterView> {
    return this.http.get<CharacterView>(`${this.baseUrl}/${userName}`);
  }
}
