import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { SaleStore } from './sale.store';
import { CustomerLookupComponent } from './components/customer-lookup.component';
import { ProductPickerComponent } from './components/product-picker.component';
import { SaleItemsTableComponent } from './components/sale-items-table.component';
import { DiscountSummaryComponent } from './components/discount-summary.component';
import { SaleActionsComponent } from './components/sale-actions.component';

@Component({
  selector: 'app-sale-page',
  providers: [SaleStore],
  imports: [
    CustomerLookupComponent,
    ProductPickerComponent,
    SaleItemsTableComponent,
    DiscountSummaryComponent,
    SaleActionsComponent,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <main class="mx-auto max-w-4xl px-4 py-10">
      <header class="mb-8">
        <h1 class="text-2xl font-semibold tracking-tight text-neutral-900">Registrar venta</h1>
        <p class="mt-1 text-sm text-neutral-500">
          Demo de patrones de diseño — Strategy &amp; Chain of Responsibility.
        </p>
      </header>

      @if (store.errorMessage(); as error) {
        <p class="mb-6 rounded-md border border-neutral-300 bg-neutral-100 px-4 py-3 text-sm text-neutral-800" role="alert">
          {{ error }}
        </p>
      }

      <div class="grid gap-6 lg:grid-cols-3">
        <div class="space-y-6 lg:col-span-2">
          <app-customer-lookup />
          <app-product-picker />
          <app-sale-items-table />
        </div>

        <div class="space-y-6">
          <app-discount-summary />
          <app-sale-actions />
        </div>
      </div>
    </main>
  `,
})
export class SalePageComponent implements OnInit {
  protected readonly store = inject(SaleStore);

  ngOnInit(): void {
    this.store.loadProducts();
  }
}
