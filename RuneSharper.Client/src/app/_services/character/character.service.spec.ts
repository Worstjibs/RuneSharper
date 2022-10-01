import { TestBed } from '@angular/core/testing';
import { CharacterListModel } from '@app/models/character';

import { CharacterService } from './character.service';

describe('CharacterService', () => {
  let service: CharacterService;
  
  var characterList = [
    {
      userName: 'Worstjibs',
      totalLevel: 2075,
      totalExperience: 178243094,
      firstTracked: new Date('2020-01-01')
    },
    {
      userName: 'Bestjibs',
      totalLevel: 901,
      totalExperience: 3110432,
      firstTracked: new Date('2020-01-01')
    },
    {
      userName: 'Mediumjibs',
      totalLevel: 1859,
      totalExperience: 121006831,
      firstTracked: new Date('2020-01-01')
    },
    {
      userName: 'Smalljibs',
      totalLevel: 1512,
      totalExperience: 31788806,
      firstTracked: new Date('2020-01-01')
    }
  ] as CharacterListModel[];

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CharacterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
