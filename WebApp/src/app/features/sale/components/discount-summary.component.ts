import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DecimalPipe } from '@angular/common';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-discount-summary',
  imports: [DecimalPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './discount-summary.component.html',
})
export class DiscountSummaryComponent {
  protected readonly store = inject(SaleStore);
}
