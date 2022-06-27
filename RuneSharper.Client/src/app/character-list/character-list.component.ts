import { Component, OnInit } from '@angular/core';
import { Character } from '@app/models/character';
import { CharacterService } from '@app/_services/character/character.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css']
})
export class CharacterListComponent implements OnInit {
  characters$: Observable<Character[]> | undefined;

  constructor(private readonly characterService : CharacterService) { }

  ngOnInit(): void {
    this.characters$ = this.characterService.getCharacterList();
  }

}
