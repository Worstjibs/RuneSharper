import { Component, Input, OnInit } from '@angular/core';
import { Stats } from '@app/models/stats.model';
import { ColumnMode, TableColumn } from '@boring.devs/ngx-datatable';

@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css']
})
export class SkillListComponent implements OnInit {
  @Input() stats: Stats;

  reorderable = true;

  columns: TableColumn[] = [];

  ColumnMode = ColumnMode;

  skills : any[]

  constructor() {  
    this.columns = [
      { prop: 'name' },
      { prop: 'level', sortable: true },
      { prop: 'experience', sortable: true },
      { prop: 'rank', sortable: true }
    ];
  }
  
  ngOnInit(): void {
    this.skills = Object.values(this.stats);
  }

  getSkill(name: string) {
    let keys = Object.keys(this.stats).find(x => x)
  }

}
