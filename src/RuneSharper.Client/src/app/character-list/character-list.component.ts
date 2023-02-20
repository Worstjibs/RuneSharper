import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CharacterList } from '@app/models/character-list.model';
import { CharacterService } from '@app/_services/character/character.service';
import { ColumnMode, SortDirection, TableColumn } from '@boring.devs/ngx-datatable';

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css']
})
export class CharacterListComponent implements OnInit {
  rows: CharacterList[] = [];
  loadingIndicator = true;
  reorderable = true;

  sorts = [{ prop: 'userName', dir: 'asc' }];

  columns: TableColumn[] = [];

  ColumnMode = ColumnMode;

  constructor(
    private readonly characterService: CharacterService,
    private readonly datePipe: DatePipe,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.columns = [
      { prop: 'userName' },
      { prop: 'totalLevel' },
      { prop: 'totalExperience' },
      { prop: 'firstTracked', pipe: this.datePipe },
      { prop: 'highestSkill', sortable: false }
    ];

    this.sort('userName', SortDirection.asc);
  }

  onSort($event: any) {
    this.sort($event.column.prop, $event.newValue);
  }

  onActivate($event: any) {
    if ($event.type == 'click') {
      let character = $event.row as CharacterList;
      this.router.navigateByUrl(`/characters/${character.userName}`);
    }
  }

  private sort(sort: string, direction: SortDirection) {
    const directionNum = Object.keys(SortDirection).indexOf(direction);

    this.characterService.getCharacterList(sort, directionNum)
      .subscribe(data => {
        this.rows = data;

        this.loadingIndicator = false;
      });
  }
}
