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

  title: string = "Weekly";
  orders: any;
  ordersLabels!: string[];
  orderData!: number;
  tempDate!: Date;
  startDate!: string;
  endDate!: string;
  selectedStartDate?: Date;
  selectedEndDate?: Date;
  showDateRangePicker: boolean = true;
  showDateMonthPicker: boolean = false;
  showDateYearPicker: boolean = false;
  currentDateRange?: string;
  currentMonth?: string;
  currentYear?: string;

  filterButtons = [
    { text: 'Weekly', isClicked: true },
    { text: 'Monthly', isClicked: false },
    { text: 'Yearly', isClicked: false },
  ]

  barChartOptions: ChartConfiguration['options'] = {
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
  barChartType: ChartType = 'bar';
  barChartPlugins = [DataLabelsPlugin];
  barChartLabels!: string[];

  barChartDataset: ChartDataset[] = [
    { data: [], label: 'Sales' }
  ];

  constructor(private _salesDataService: SalesDataService) { }

  ngOnInit() { }

  updateBarChartData($event: any, mode: string) {
    if (mode == "Weekly" || mode == "Monthly") {
      this.startDate = moment($event.startDate, "MM/DD/YYYY").format("YYYYMMDD").toString();
      this.endDate = moment($event.endDate, "MM/DD/YYYY").format("YYYYMMDD").toString();

      this._salesDataService.getOrdersByDate(this.startDate, this.endDate)
        .subscribe(
          (res) => {
            const localChartData = this.getChartData(res.data);

            this.barChartLabels = localChartData.map((x: any[]) => x[0]);
            this.barChartDataset[0].data = localChartData.map((x: any[]) => x[1]);
            this.chart?.update();
          }
        );
    }
    else if (mode == "Yearly") {
      this.startDate = moment($event.startDate, "MM/DD/YYYY").format("YYYYMMDD").toString();
      this.endDate = moment($event.endDate, "MM/DD/YYYY").format("YYYYMMDD").toString();

      this._salesDataService.getOrdersByDate(this.startDate, this.endDate)
        .subscribe(
          (res) => {
            const localChartData = this.getChartData(res.data);

            interface GroupedData {
              [key: string]: number;
            }

            const groupedDates: GroupedData = localChartData.reduce((acc: any, obj: any) => {
              const tempDate = new Date(obj[0]);
              const monthYear = this.getMonthName(tempDate.getMonth() + 1);

              if (!acc[monthYear]) {
                acc[monthYear] = 0;
              }

              acc[monthYear] += obj[1];

              return acc;
            }, {});

            this.barChartLabels = Object.keys(groupedDates);
            this.barChartDataset[0].data = this.barChartLabels.map(key => groupedDates[key]);
            this.chart?.update();
          }
        );
    }
  }

  getMonthName(monthNumber: number): string {
    const months = [
      'January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'
    ];

    return months[monthNumber - 1];
  }

  getChartData(res: any) {
    this.orders = res.page.data;

    const formattedOrders = this.orders.reduce((data: any[], order: any) => {
      data.push([moment(order.date).format('YY-MM-DD'), order.total]);
      return data;
    }, []);

    const temp: any = [];

    const chartData = formattedOrders.reduce((data: any[], order: any[]) => {
      const key = order[0];

      if (!temp[key]) {
        temp[key] = order;
        data.push(temp[key]);
      } else {
        temp[key][1] += order[1];
      }

      return data;
    }, []);

    return chartData;
  }

  eventHandler(button: any): void {
    for (let but of this.filterButtons) {
      but.isClicked = false;
    }

    this.title = button.text;

    button.isClicked = true;
    if (button.text == "Weekly") {
      this.showDateRangePicker = true;
      this.showDateMonthPicker = false;
      this.showDateYearPicker = false;
    }
    else if (button.text == "Monthly") {
      this.showDateRangePicker = false;
      this.showDateMonthPicker = true;
      this.showDateYearPicker = false;
    }
    else if (button.text == "Yearly") {
      this.showDateRangePicker = false;
      this.showDateMonthPicker = false;
      this.showDateYearPicker = true;
    }
  }
}
