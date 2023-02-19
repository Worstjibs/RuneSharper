import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CharacterView } from '@app/models/character-view.model';
import { FrequencyType } from '@app/models/frequency';

@Component({
  selector: 'app-character-view',
  templateUrl: './character-view.component.html',
  styleUrls: ['./character-view.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CharacterViewComponent implements OnInit {
  character: CharacterView | undefined;

  Frequency = FrequencyType;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data: any) => {
      if (data.character == undefined)
        return;

      this.character = data.character as CharacterView;
    });
  }
}
