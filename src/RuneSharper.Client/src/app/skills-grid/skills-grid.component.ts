import { Component, Input } from '@angular/core';
import { Stats } from '@app/models/stats.model';

@Component({
  selector: 'app-skills-grid',
  templateUrl: './skills-grid.component.html',
  styleUrls: ['./skills-grid.component.css']
})
export class SkillsGridComponent {
  @Input() stats: Stats;

  constructor() { }
}
