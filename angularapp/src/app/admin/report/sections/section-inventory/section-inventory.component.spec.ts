import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionInventoryComponent } from './section-inventory.component';

describe('SectionInventoryComponent', () => {
  let component: SectionInventoryComponent;
  let fixture: ComponentFixture<SectionInventoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SectionInventoryComponent]
    });
    fixture = TestBed.createComponent(SectionInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
