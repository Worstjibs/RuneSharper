import { Component, OnInit } from '@angular/core';
import { LineChartModel } from '@app/models/line-chart-model';
import { Color, ColorHelper, ScaleType } from '@swimlane/ngx-charts';
import { map, Observable, of } from 'rxjs';
import { LineChartService } from '../_services/line-chart/line-chart.service';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {
  initMulti: LineChartModel[] | undefined;
  multi: Observable<LineChartModel[]> | undefined;
  view: [number, number] = [1000, 800];

  legendData: string[] = [];

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

  chartColors: any;

  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  } as Color;

  constructor(private readonly lineChartService: LineChartService) {
    //Object.assign(this, { multi });
  }

  ngOnInit(): void {
    const dateTo = new Date();
    const dateFrom = new Date();
    dateFrom.setDate(dateTo.getDate() - 90);

    this.multi = this.lineChartService.getData('worstjibs', dateFrom, dateTo, true);
    this.multi.subscribe(x => {
      this.initMulti = x;
      this.legendData = x.map(y => y.name);

      this.chartColors = new ColorHelper('neons', ScaleType.Ordinal, this.legendData, null);
      this.colorScheme.domain = this.chartColors.colorDomain;
    });
  }

  onSelect(data: any): void {
    if (this.isLegend(data)) {
      const modelName = data as string;

      this.multi = this.multi?.pipe(
        map(model => {
          const selectedModel = model.find(x => x.name === modelName);

          if (selectedModel?.series.length === 0) {
            const initModel = this.initMulti?.find(x => x.name == modelName);
            if (initModel) {
              selectedModel.series = initModel.series;
            }
          } else {
            model = model.map(d => {
              if (d.name === data) {
                d.series = [];
              }
              return d;
            });
          }
          
          return model
        })
      )
    }
  }

  onActivate(data: any): void {
    // console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data: any): void {
    // console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  isLegend = (event : any) => typeof event === 'string';
}
