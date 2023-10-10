import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateMonthPickerComponent } from './date-month-picker.component';

describe('DateMonthPickerComponent', () => {
  let component: DateMonthPickerComponent;
  let fixture: ComponentFixture<DateMonthPickerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateMonthPickerComponent]
    });
    fixture = TestBed.createComponent(DateMonthPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
