import { Component, OnInit } from '@angular/core';
import { LineChartModel } from '@app/models/line-chart-model';
import { Color } from '@swimlane/ngx-charts';
import { Observable } from 'rxjs';
import { LineChartService } from '../_services/line-chart/line-chart.service';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {
  multi: Observable<LineChartModel[]> | undefined;
  view: [number, number] = [1000, 500];

  data: any;

  // options
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Year';
  yAxisLabel: string = 'Experience';
  timeline: boolean = true;

  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  } as Color;

  constructor(private readonly lineChartService: LineChartService) {
    //Object.assign(this, { multi });
  }

  ngOnInit(): void {
    const dateTo = new Date();
    const dateFrom = new Date();
    dateFrom.setDate(dateTo.getDate() - 30);

    this.multi = this.lineChartService.getData('worstjibs', dateFrom, dateTo, false)
      // .pipe(
      //   map(data => {
      //     return data.filter(d => d.name === 'Overall')
      //   })
      // )
      // .subscribe(data => {
      //   this.multi = data;
      // });
  }

  onSelect(data: any): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data: any): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data: any): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }
}
