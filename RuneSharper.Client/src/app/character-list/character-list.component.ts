import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CharacterListModel } from '@app/models/character';
import { CharacterService } from '@app/_services/character/character.service';
import { ColumnMode, TableColumn } from '@boring.devs/ngx-datatable';

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

  ColumnMode = ColumnMode;

}
