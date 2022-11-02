import { DatePipe } from '@angular/common';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CharacterList } from '@app/models/character-list.model';
import { CharacterService } from '@app/_services/character/character.service';
import { of } from 'rxjs';

import { CharacterListComponent } from './character-list.component';

describe('CharacterListComponent', () => {
  let component: CharacterListComponent;
  let fixture: ComponentFixture<CharacterListComponent>;

  let characterList = [
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
  ] as CharacterList[];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('CharacterService', ['getCharacterList']);
    spy.getCharacterList.and.returnValue(of(characterList));

    await TestBed.configureTestingModule({
      imports: [],
      providers: [
        DatePipe,
        { provide: CharacterService, useValue: spy }
      ],
      declarations: [CharacterListComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CharacterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
