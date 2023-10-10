import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateYearPickerComponent } from './date-year-picker.component';

describe('DateYearPickerComponent', () => {
  let component: DateYearPickerComponent;
  let fixture: ComponentFixture<DateYearPickerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateYearPickerComponent]
    });
    fixture = TestBed.createComponent(DateYearPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
