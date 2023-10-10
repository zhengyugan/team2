import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionHealthComponent } from './section-health.component';

describe('SectionHealthComponent', () => {
  let component: SectionHealthComponent;
  let fixture: ComponentFixture<SectionHealthComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SectionHealthComponent]
    });
    fixture = TestBed.createComponent(SectionHealthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
