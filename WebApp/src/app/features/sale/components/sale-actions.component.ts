import { ChangeDetectionStrategy, Component, inject } from '@angular/core';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-sale-actions',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './sale-actions.component.html',
})
export class SaleActionsComponent {
  protected readonly store = inject(SaleStore);
}
