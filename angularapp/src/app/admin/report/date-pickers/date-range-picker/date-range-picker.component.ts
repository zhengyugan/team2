import { Component, EventEmitter, Injectable, Input, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DateRange, MAT_DATE_RANGE_SELECTION_STRATEGY, MatDateRangeSelectionStrategy } from '@angular/material/datepicker';

@Injectable()
export class SundayToSaturdayRangeSelectionStrategy<D> implements MatDateRangeSelectionStrategy<D> {
  constructor() { }

  selectionFinished(date: any): DateRange<D> {
    const startDate = this.getPreviousSunday(date || new Date());
    const endDate = this.getFollowingSaturday(startDate);
    return new DateRange<D>(startDate, endDate);
  }

  createPreview(active: D): DateRange<D> {
    const startDate = this.getPreviousSunday(active);
    const endDate = this.getFollowingSaturday(startDate);
    return new DateRange<D>(startDate, endDate);
  }

  private getPreviousSunday(date: D): D {
    const result = new Date(date as any);
    result.setDate((date as any).getDate() - (date as any).getDay());
    return result as any;
  }

  private getFollowingSaturday(date: D): D {
    const result = new Date(date as any);
    result.setDate((date as any).getDate() + 6);
    return result as any;
  }
}

@Component({
  selector: 'app-date-range-picker',
  templateUrl: './date-range-picker.component.html',
  styleUrls: ['./date-range-picker.component.css'],
  providers: [
    {
      provide: MAT_DATE_RANGE_SELECTION_STRATEGY,
      useClass: SundayToSaturdayRangeSelectionStrategy,
    },
  ]
})
export class DateRangePickerComponent {
  @Output() actionEvent = new EventEmitter<any>();
  @Input() currentDateRange?: string;

  currentDate = new Date();

  dateRange = new FormGroup({
    start: new FormControl(this.getPreviousSunday()),
    end: new FormControl(this.getFollowingSaturday())
  });

  constructor() { }

  onDateChange(dateStart: HTMLInputElement, dateEnd: HTMLInputElement) {
    if (dateStart.value && dateEnd.value)
      this.actionEvent.emit({ startDate: dateStart.value, endDate: dateEnd.value });
  }

  ngOnChanges(changes: SimpleChanges) {
    this.actionEvent.emit({ startDate: this.dateRange.value.start?.toLocaleDateString(), endDate: this.dateRange.value.end?.toLocaleDateString() });
  }

  getPreviousSunday() {
    const result = new Date();
    result.setDate(result.getDate() - result.getDay());
    return result;
  }

  getFollowingSaturday() {
    const result = new Date();
    result.setDate(result.getDate() - result.getDay() + 6);
    return result;
  }
}
