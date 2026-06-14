import { ChangeDetectionStrategy, Component, effect, inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-customer-lookup',
  imports: [ReactiveFormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './customer-lookup.component.html',
})
export class CustomerLookupComponent {
  protected readonly store = inject(SaleStore);
  protected readonly nitControl = new FormControl('', { nonNullable: true });
  protected readonly nameControl = new FormControl('', { nonNullable: true });

  constructor() {
    this.nitControl.valueChanges.subscribe((value) => this.store.setNit(value));
    this.nameControl.valueChanges.subscribe((value) => this.store.setNewCustomerName(value));

    // Reflect external resets of the store back into the form controls.
    effect(() => {
      const nit = this.store.nit();
      if (nit !== this.nitControl.value) {
        this.nitControl.setValue(nit, { emitEvent: false });
      }
      const name = this.store.newCustomerName();
      if (name !== this.nameControl.value) {
        this.nameControl.setValue(name, { emitEvent: false });
      }
    });
  }

  protected onSearch(event: Event): void {
    event.preventDefault();
    this.store.searchCustomer();
  }
}
