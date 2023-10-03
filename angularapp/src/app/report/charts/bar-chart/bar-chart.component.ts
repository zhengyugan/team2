import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartDataset, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { SalesDataService } from '../../services/sales-data.service';

import DataLabelsPlugin from 'chartjs-plugin-datalabels';
import * as moment from 'moment';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  orders: any;
  ordersLabels!: string[];
  orderData!: number;

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
      },
      datalabels: {
        anchor: 'end',
        align: 'end',
      },
    },
  };
  public barChartType: ChartType = 'bar';
  public barChartPlugins = [DataLabelsPlugin];
  public barChartLabels!: string[];

  public barChartDataset: ChartDataset[] = [
    { data: [], label: 'Sales' }
  ];

  constructor(private _salesDataService: SalesDataService) { }

  ngOnInit() {
    this._salesDataService.getOrders(1, 100)
      .subscribe(
        (res) => {
          const localChartData = this.getChartData(res);
          this.barChartLabels = localChartData.map((x: any[]) => x[0]).reverse();
          this.barChartDataset[0].data = localChartData.map((x: any[]) => x[1]);
          this.chart?.update();
        }
      );
  }

  getChartData(res: any) {
    this.orders = res.page.data;
    const data = this.orders.map((o: { total: any; }) => o.total);
    const labels = this.orders.map((o: { placed: string | number | Date; }) => moment(new Date(o.placed)).format('YY-MM-DD'));

    const formattedOrders = this.orders.reduce((r: any[], e: any) => {
      r.push([moment(e.placed).format('YY-MM-DD'), e.total]);
      return r;
    }, []);

    const p: any = [];

    const chartData = formattedOrders.reduce((r: any[], e: any[]) => {
      const key = e[0];

      if (!p[key]) {
        p[key] = e;
        r.push(p[key]);
      } else {
        p[key][1] += e[1];
      }

      return r;
    }, []);

    return chartData;
  }
}
