import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProductListComponent } from './product-list.component';

describe('AdminProductListComponent', () => {
  let component: AdminProductListComponent;
  let fixture: ComponentFixture<AdminProductListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminProductListComponent]
    });
    fixture = TestBed.createComponent(AdminProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
