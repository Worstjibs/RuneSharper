import { Injectable } from '@angular/core';
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { CharacterView } from '@app/models/character-view.model';
import { CharacterService } from '@app/_services/character/character.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CharacterViewResolver implements Resolve<CharacterView> {
  constructor(private readonly characterService: CharacterService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CharacterView> {
    return this.characterService.getCharacterView(route.params['userName']);
  }
}
