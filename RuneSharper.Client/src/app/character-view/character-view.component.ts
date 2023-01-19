import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Activity } from '@app/models/activities.model';
import { CharacterView } from '@app/models/character-view.model';
import { Frequency } from '@app/enums/frequency';
import { Skill, Stats } from '@app/models/stats.model';

@Component({
  selector: 'app-character-view',
  templateUrl: './character-view.component.html',
  styleUrls: ['./character-view.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CharacterViewComponent implements OnInit {
  character: CharacterView | undefined;

  Frequency = Frequency;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data: any) => {
      if (data.character == undefined)
        return;

      this.character = data.character as CharacterView;
    });
  }

  getBosses(frequency: Frequency) : Activity[] {
    return this.character?.activitiesChange.find(x => x.frequency == frequency)?.model.bosses ?? [];
  }

  getStats(frequency: Frequency): Stats {
    return this.character?.statsChange.find(x => x.frequency ==  frequency)?.model ?? {} as Stats;
  }
}
