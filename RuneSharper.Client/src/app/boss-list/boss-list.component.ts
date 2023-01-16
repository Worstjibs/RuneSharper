import { Component, Input, OnInit } from '@angular/core';
import { Activity } from '@app/models/activities.model';
import { ColumnMode, TableColumn } from '@boring.devs/ngx-datatable';

@Component({
  selector: 'app-boss-list',
  templateUrl: './boss-list.component.html',
  styleUrls: ['./boss-list.component.css']
})
export class BossListComponent implements OnInit {
  @Input() bosses: Activity[];

  loadingIndicator = true;
  reorderable = true;

  columns: TableColumn[] = [];

  ColumnMode = ColumnMode;

  constructor() { }

  ngOnInit(): void {
    this.columns = [
      { prop: 'name' },
      { prop: 'score', sortable: true },
      { prop: 'rank' },
    ]
  }

}
