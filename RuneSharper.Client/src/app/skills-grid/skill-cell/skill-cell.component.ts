import { Component, Input, OnInit } from '@angular/core';
import { Skill } from '@app/models/skill.model';

@Component({
  selector: 'app-skill-cell',
  templateUrl: './skill-cell.component.html',
  styleUrls: ['./skill-cell.component.css']
})
export class SkillCellComponent {
  @Input() skill: Skill;
  @Input() skillName: string;

  get imagePath() {
    return `assets/${this.skillName}_icon.png`;
  }

  constructor() {
  }
}
