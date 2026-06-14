import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DecimalPipe } from '@angular/common';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-sale-items-table',
  imports: [DecimalPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './sale-items-table.component.html',
})
export class SaleItemsTableComponent {
  protected readonly store = inject(SaleStore);
}
