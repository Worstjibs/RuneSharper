import { Injectable } from '@angular/core';
import { Character } from '@app/models/character';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  constructor() { }

  getCharacterList() : Observable<Character[]> {
    var characterList = [
      {
        name: 'Worstjibs',
        totalLevel: 2075,
        totalExperience: 178243094,
        dateCreated: new Date('2020-01-01')
      },
      {
        name: 'Bestjibs',
        totalLevel: 901,
        totalExperience: 3110432,
        dateCreated: new Date('2020-01-01')
      },
      {
        name: 'Mediumjibs',
        totalLevel: 1859,
        totalExperience: 121006831,
        dateCreated: new Date('2020-01-01')
      },
      {
        name: 'Smalljibs',
        totalLevel: 1512,
        totalExperience: 31788806,
        dateCreated: new Date('2020-01-01')
      }
    ] as Character[];

    return of(characterList);
  }
}
