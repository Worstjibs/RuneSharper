import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CharacterListModel } from '@app/models/character-list-model';
import { CharacterService } from '@app/_services/character/character.service';
import { ColumnMode, SortDirection, TableColumn } from '@boring.devs/ngx-datatable';

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css']
})
export class CharacterListComponent implements OnInit {
  rows: CharacterListModel[] = [];
  loadingIndicator = true;
  reorderable = true;

  columns: TableColumn[] = [];

  ColumnMode = ColumnMode;

  constructor(
    private readonly characterService: CharacterService,
    private readonly datePipe: DatePipe) { }

  ngOnInit(): void {
    this.columns = [
      { prop: 'userName' },
      { prop: 'totalLevel' },
      { prop: 'totalExperience' },
      { prop: 'firstTracked', pipe: this.datePipe }
    ];

    this.characterService.getCharacterList()
      .subscribe(data => {
        this.rows = data;

        this.loadingIndicator = false;
      });
  }

  onSort($event: any) {
    this.sort($event.column.prop, $event.newValue);
  }

  sort(sort: string, direction: SortDirection) {
    const directionNum = Object.keys(SortDirection).indexOf(direction);

    this.characterService.getCharacterList(sort, directionNum)
      .subscribe(data => {
        this.rows = data;

        this.loadingIndicator = false;
      });
  }


}
