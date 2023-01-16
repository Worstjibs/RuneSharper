import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Activities } from '@app/models/activities.model';
import { CharacterView } from '@app/models/character-view.model';
import { Frequency } from '@app/models/frequency';

@Component({
  selector: 'app-character-view',
  templateUrl: './character-view.component.html',
  styleUrls: ['./character-view.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CharacterViewComponent implements OnInit {
  character: CharacterView | undefined;

  frequency = Frequency;

  activitiesChangeMap: Map<Frequency, Activities>;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data: any) => {
      if (data.character == undefined)
        return;

      this.character = data.character as CharacterView;

      this.activitiesChangeMap = new Map();

      this.character.activitiesChange.forEach(x => {
        this.activitiesChangeMap.set(x.key, x.value);
      });
    });
  }
}
