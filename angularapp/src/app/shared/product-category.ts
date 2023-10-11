export interface ProductCategory {
  id: number;
  name: string;
  desc: string;
}
export interface Product {
  id: number;
  name: string;
  desc: string;
  productCategory: ProductCategory;
}

export interface ProductVariant {
  id: number;
  quantity: number;
  size: string;
  color: string;
  length: string;
  product: Product;
}
