import { Injectable, WritableSignal, computed, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';

import { Customer } from '../../core/models/customer.model';
import { Product } from '../../core/models/product.model';
import { Discount, RegisterSaleRequest, SaleResult } from '../../core/models/sale.model';
import { CustomersService } from '../../core/services/customers.service';
import { ProductsService } from '../../core/services/products.service';
import { SalesService } from '../../core/services/sales.service';

export interface SaleLineItem {
  product: Product;
  quantity: number;
}

type Status = 'idle' | 'loading' | 'error';

/**
 * ViewModel for the register-sale flow. Holds all UI state as signals and
 * exposes derived state via computed signals, keeping the components purely
 * presentational (MVVM).
 */
@Injectable()
export class SaleStore {
  private readonly customersService = inject(CustomersService);
  private readonly productsService = inject(ProductsService);
  private readonly salesService = inject(SalesService);

  // --- State ---
  readonly nit = signal('');
  readonly customer = signal<Customer | null>(null);
  readonly customerStatus = signal<Status>('idle');
  readonly newCustomerName = signal('');
  readonly customerSearched = signal(false);

  readonly products = signal<Product[]>([]);
  readonly items = signal<SaleLineItem[]>([]);

  readonly discountResult = signal<SaleResult | null>(null);
  readonly discountStatus = signal<Status>('idle');

  readonly registeredSale = signal<SaleResult | null>(null);
  readonly registerStatus = signal<Status>('idle');

  readonly errorMessage = signal<string | null>(null);

  // --- Derived state ---
  readonly isNewCustomer = computed(() => this.customerSearched() && this.customer() === null);

  readonly customerFullName = computed(() =>
    this.customer()?.fullName ?? this.newCustomerName().trim(),
  );

  readonly subtotal = computed(() =>
    this.items().reduce((sum, line) => sum + line.product.unitCost * line.quantity, 0),
  );

  readonly discount = computed<Discount | null>(() => this.discountResult()?.discount ?? null);

  readonly total = computed(() => this.subtotal() - (this.discount()?.amount ?? 0));

  readonly hasItems = computed(() => this.items().length > 0);

  readonly canCalculate = computed(
    () => this.customerSearched() && this.hasItems() && this.customerFullName().length > 0,
  );

  readonly canPersist = computed(() => this.canCalculate() && this.registeredSale() === null);

  // --- Actions ---
  loadProducts(): void {
    this.productsService.list().subscribe({
      next: (products) => this.products.set(products),
      error: () => this.errorMessage.set('No se pudieron cargar los productos.'),
    });
  }

  setNit(nit: string): void {
    this.nit.set(nit);
    this.customerSearched.set(false);
    this.customer.set(null);
    this.discountResult.set(null);
  }

  searchCustomer(): void {
    const nit = this.nit().trim();
    if (nit.length === 0) return;

    this.runRequest(
      this.customerStatus,
      this.customersService.getByNit(nit),
      (customer) => {
        this.customer.set(customer);
        this.customerSearched.set(true);
      },
      'Error al buscar el cliente.',
    );
  }

  setNewCustomerName(name: string): void {
    this.newCustomerName.set(name);
  }

  addItem(product: Product, quantity: number): void {
    if (quantity < 1) return;
    this.items.update((lines) => this.upsertItem(lines, product, quantity));
    this.discountResult.set(null);
  }

  removeItem(productId: string): void {
    this.items.update((lines) => lines.filter((line) => line.product.id !== productId));
    this.discountResult.set(null);
  }

  calculateDiscount(): void {
    if (!this.canCalculate()) return;

    this.runRequest(
      this.discountStatus,
      this.salesService.calculateDiscount(this.buildRequest()),
      (result) => this.discountResult.set(result),
      'No se pudo calcular el descuento.',
    );
  }

  registerSale(): void {
    if (!this.canPersist()) return;

    this.runRequest(
      this.registerStatus,
      this.salesService.register(this.buildRequest()),
      (result) => {
        this.registeredSale.set(result);
        this.discountResult.set(result);
      },
      'No se pudo registrar la venta.',
    );
  }

  reset(): void {
    this.nit.set('');
    this.customer.set(null);
    this.newCustomerName.set('');
    this.customerSearched.set(false);
    this.items.set([]);
    this.discountResult.set(null);
    this.registeredSale.set(null);
    this.customerStatus.set('idle');
    this.discountStatus.set('idle');
    this.registerStatus.set('idle');
    this.errorMessage.set(null);
  }

  private buildRequest(): RegisterSaleRequest {
    return {
      customerNit: this.nit().trim(),
      customerFullName: this.customerFullName(),
      items: this.items().map((line) => ({
        productId: line.product.id,
        quantity: line.quantity,
      })),
    };
  }

  private upsertItem(lines: SaleLineItem[], product: Product, quantity: number): SaleLineItem[] {
    const exists = lines.some((l) => l.product.id === product.id);
    if (exists) {
      return lines.map((l) =>
        l.product.id === product.id ? { ...l, quantity: l.quantity + quantity } : l,
      );
    }
    return [...lines, { product, quantity }];
  }

  private runRequest<T>(
    statusSignal: WritableSignal<Status>,
    request$: Observable<T>,
    onNext: (result: T) => void,
    errorMsg: string,
  ): void {
    statusSignal.set('loading');
    this.errorMessage.set(null);
    request$.subscribe({
      next: (result) => {
        onNext(result);
        statusSignal.set('idle');
      },
      error: () => {
        statusSignal.set('error');
        this.errorMessage.set(errorMsg);
      },
    });
  }
}
