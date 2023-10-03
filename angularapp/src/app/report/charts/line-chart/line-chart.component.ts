import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartDataset } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { LINE_CHART_COLORS } from '../../../shared/chart.colors';

import { SalesDataService } from '../../services/sales-data.service';
import * as moment from 'moment';
import { Color } from 'chartjs-plugin-datalabels/types/options';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  lineChartLabels!: string[];
  lineChartDataset: ChartDataset[] = [];

  lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    elements: {
      line: {
        tension: 0.5,
      },
    },

    plugins: {
      legend: {
        display: true,
      },
    },
  };

  lineChartColors: Color[] = LINE_CHART_COLORS;

  topCustomers?: string[];
  allOrders?: any[];

  constructor(private _salesDataService: SalesDataService) { }

  ngOnInit(): void {
    this._salesDataService.getOrders(1, 100)
      .subscribe(
        (res) => {
          this.allOrders = res.page.data;
          this._salesDataService.getOrdersByCustomer(3)
            .subscribe(
              (cus: any) => {
                this.topCustomers = cus.map((x: any) => x.name);

                const allChartData = this.topCustomers?.reduce((result: any, i: string) => {
                  result.push(this.getChartData(this.allOrders, i));
                  return result;
                }, []);

                let dates = allChartData.map((x: any) => x.data).reduce((a: any[], i: any[][]) => {
                  a.push(i.map((o: any[]) => new Date(o[0])));
                  return a;
                }, []);

                dates = [].concat.apply([], dates);

                const r: any[] = this.getCustomerOrdersByDate(allChartData, dates).data;

                this.lineChartLabels = r[0].orders.map((o: any) => o.date);
                this.lineChartDataset = [
                  { 'data': r[0].orders.map((x: any) => x.total), 'label': r[0].customer, fill: 'origin' },
                  { 'data': r[1].orders.map((x: any) => x.total), 'label': r[1].customer, fill: 'origin' },
                  { 'data': r[2].orders.map((x: any) => x.total), 'label': r[2].customer, fill: 'origin' },
                ]
              });
        });
  }

  getChartData(allOrders: any, name: string) {
    const customerOrders = allOrders.filter((o: { customer: { name: string; }; }) => o.customer.name === name);
    const formattedOrders = customerOrders.reduce((r: any[][], e: { placed: any; total: any; }) => {
      r.push([e.placed, e.total]);
      return r;
    }, [])

    const result = { customer: name, data: formattedOrders };

    return result;
  }

  getCustomerOrdersByDate(orders: any, dates: any) {
    const customers = this.topCustomers;
    const prettyDates = dates.map((x: Date) => this.toFriendlyDate(x));
    const u = Array.from(new Set(prettyDates)).sort();

    const result = { 'data': [] };
    const datasets:any[] = result['data'] = [];

    customers!.reduce((x, y, i: number) => {
      const customerOrders: any = [];

      datasets[i] = {
        customer: y,
        orders: u.reduce((r, e: any, j: number) => {
          const obj: any = { date: [], total: [] };

          obj.date = e;
          obj.total = this.getCustomerDateTotal(e, y);
          customerOrders.push(obj);

          return customerOrders;
        })
      };

      return x;
    }, []);

    return result;
  }

  toFriendlyDate(date: Date) {
    return moment(date).endOf('day').format('YY-MM-DD');
  }

  getCustomerDateTotal(date: string, customer: string) {
    const r = this.allOrders?.filter(o => o.customer.name === customer && this.toFriendlyDate(o.placed) === date);
    const result = r?.reduce((a, b) => {
      return a + b.total;
    }, 0);

    return result;
  }
}
