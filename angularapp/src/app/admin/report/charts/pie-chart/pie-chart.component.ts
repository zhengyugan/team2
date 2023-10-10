import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartDataset, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { THEME_COLORS } from '../../../../shared/theme.colors';

import DatalabelsPlugin from 'chartjs-plugin-datalabels';
import * as _ from 'lodash';

const theme = 'Default';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'top',
      },
      datalabels: {
        formatter: (value: any, ctx: any) => {
          if (ctx.chart.data.labels) {
            return ctx.chart.data.labels[ctx.dataIndex];
          }
        },
      },
    },
  };

  public pieChartLabels!: string[];
  public pieChartDataset: ChartDataset[] = [
    {
      data: [],
      backgroundColor: this.themeColors(theme),
      borderColor: '#111'
    }
  ];

  public pieChartType: ChartType = 'doughnut';
  public pieChartPlugins = [DatalabelsPlugin];

  @Input() inputData: any;
  @Input() limit!: number;
  constructor() { }

  ngOnInit(): void {
    this.parseChartData(this.inputData, this.limit);
  }

  parseChartData(res: any, limit: number) {
    const allData = res.slice(0, limit);
    this.pieChartDataset[0].data = allData.map((x: { [x: string]: any; }) => _.values(x)[1]);
    this.pieChartLabels = allData.map((x: { [x: string]: any; }) => _.values(x)[0]);
    this.themeColors(theme);
  }

  themeColors(setName: string): string[] {
    const c = THEME_COLORS.slice(0).find(set => set.name === setName)!.colorSet;
    return c;
  }
}
