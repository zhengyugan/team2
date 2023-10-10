import { Component, EventEmitter, Input, Output, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import * as moment from 'moment';
import { Moment } from 'moment';

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-date-month-picker',
  templateUrl: './date-month-picker.component.html',
  styleUrls: ['./date-month-picker.component.css'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
  encapsulation: ViewEncapsulation.None,
})
export class DateMonthPickerComponent {
  @Output() actionEvent = new EventEmitter<any>();
  @Input() currentMonth?: string;

  date = new FormControl(moment());

  constructor() { }

  setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.date.value!;
    ctrlValue.month(normalizedMonthAndYear.month());
    ctrlValue.year(normalizedMonthAndYear.year());
    this.date.setValue(ctrlValue);
    datepicker.close();

    const { firstDay, lastDay } = this.getFirstAndLastDayOfMonth(normalizedMonthAndYear.year(), normalizedMonthAndYear.month());
    this.actionEvent.emit({ startDate: firstDay.toLocaleDateString(), endDate: lastDay.toLocaleDateString() });
  }

  getFirstAndLastDayOfMonth(year: any, month: any): { firstDay: Date, lastDay: Date } {
    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);

    return { firstDay, lastDay };
  }

  ngOnChanges(changes: SimpleChanges) {
    const { firstDay, lastDay } = this.getFirstAndLastDayOfMonth(this.date.value?.year(), this.date.value?.month());
    this.actionEvent.emit({ startDate: firstDay.toLocaleDateString(), endDate: lastDay.toLocaleDateString() });
  }
}
