import { Component, EventEmitter, Input, Output, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import * as moment from 'moment';
import { Moment } from 'moment';

export const MY_FORMATS = {
  parse: {
    dateInput: 'YYYY',
  },
  display: {
    dateInput: 'YYYY',
    yearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    yearA11yLabel: 'YYYY',
  },
};

@Component({
  selector: 'app-date-year-picker',
  templateUrl: './date-year-picker.component.html',
  styleUrls: ['./date-year-picker.component.css'],
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
export class DateYearPickerComponent {
  @Output() actionEvent = new EventEmitter<any>();
  @Input() currentYear?: string;

  date = new FormControl(moment());

  constructor() { }

  setYear(normalizedYear: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.date.value!;
    ctrlValue.year(normalizedYear.year());
    this.date.setValue(ctrlValue);
    datepicker.close();

    const { firstDay, lastDay } = this.getFirstAndLastDayOfYear(normalizedYear.year());
    this.actionEvent.emit({ startDate: firstDay.toLocaleDateString(), endDate: lastDay.toLocaleDateString() });
  }

  getFirstAndLastDayOfYear(year: any): { firstDay: Date, lastDay: Date } {
    const firstDay = new Date(year, 0, 1);
    const lastDay = new Date(year, 11, 31);

    return { firstDay, lastDay };
  }

  ngOnChanges(changes: SimpleChanges) {
    const { firstDay, lastDay } = this.getFirstAndLastDayOfYear(this.date.value?.year());
    this.actionEvent.emit({ startDate: firstDay.toLocaleDateString(), endDate: lastDay.toLocaleDateString() });
  }
}
