import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProductCreateComponent } from './product-create.component';

describe('AdminProductCreateComponent', () => {
  let component: AdminProductCreateComponent;
  let fixture: ComponentFixture<AdminProductCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminProductCreateComponent]
    });
    fixture = TestBed.createComponent(AdminProductCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
