export interface SaleItemRequest {
  productId: string;
  quantity: number;
}

export interface RegisterSaleRequest {
  customerFullName: string;
  customerNit: string;
  items: SaleItemRequest[];
}

export interface SaleResultItem {
  productId: string;
  productName: string;
  unitCost: number;
  quantity: number;
  subtotal: number;
}

export interface Discount {
  message: string;
  amount: number;
}

export interface SaleResult {
  saleId: string | null;
  customerNit: string;
  customerFullName: string;
  isNewCustomer: boolean;
  items: SaleResultItem[];
  subtotal: number;
  discount: Discount | null;
  total: number;
}
