export interface SaleItemRequest {
  productId: string;
  quantity: number;
}

export interface RegisterSaleRequest {
  customerFullName: string;
  customerNit: string;
  items: SaleItemRequest[];
}

export interface Discount {
  message: string;
  amount: number;
}

export interface SaleResult {
  saleId: string | null;
  subtotal: number;
  discount: Discount | null;
}
