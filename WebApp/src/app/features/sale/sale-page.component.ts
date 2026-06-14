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
  templateUrl: './sale-page.component.html',
})
export class SalePageComponent implements OnInit {
  protected readonly store = inject(SaleStore);

  ngOnInit(): void {
    this.store.loadProducts();
  }
}
