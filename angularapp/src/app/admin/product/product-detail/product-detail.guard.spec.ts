import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProductDetailComponent } from '../product-detail/product-detail.component';

describe('ProductDetailComponent', () => {
  let component: AdminProductDetailComponent;
  let fixture: ComponentFixture<AdminProductDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminProductDetailComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(AdminProductDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});