import { TestBed } from '@angular/core/testing';
import { ProductDetailGuard } from 'src/app/users/product/product-detail.guard';


describe('ProductDetailGuard', () => {
  let guard: ProductDetailGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ProductDetailGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
