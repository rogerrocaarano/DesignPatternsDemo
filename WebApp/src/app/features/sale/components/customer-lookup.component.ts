import { ChangeDetectionStrategy, Component, effect, inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-customer-lookup',
  imports: [ReactiveFormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <section class="rounded-lg border border-neutral-200 bg-white p-5">
      <h2 class="text-sm font-semibold tracking-wide text-neutral-500 uppercase">Cliente</h2>

      <form class="mt-4 flex flex-wrap items-end gap-3" (submit)="onSearch($event)">
        <div class="flex-1 min-w-48">
          <label for="nit" class="block text-sm font-medium text-neutral-700">NIT</label>
          <input
            id="nit"
            type="text"
            inputmode="numeric"
            autocomplete="off"
            [formControl]="nitControl"
            class="mt-1 w-full rounded-md border border-neutral-300 bg-white px-3 py-2 text-neutral-900 shadow-sm focus:border-neutral-900 focus:ring-1 focus:ring-neutral-900 focus:outline-none"
          />
        </div>
        <button
          type="submit"
          [disabled]="nitControl.invalid || store.customerStatus() === 'loading'"
          class="rounded-md bg-neutral-900 px-4 py-2 text-sm font-medium text-white transition hover:bg-neutral-700 disabled:cursor-not-allowed disabled:bg-neutral-300"
        >
          {{ store.customerStatus() === 'loading' ? 'Buscando…' : 'Buscar' }}
        </button>
      </form>

      @if (store.customerSearched()) {
        @if (store.customer(); as customer) {
          <p class="mt-4 rounded-md bg-neutral-100 px-3 py-2 text-sm text-neutral-800" role="status">
            Cliente existente: <span class="font-semibold">{{ customer.fullName }}</span>
          </p>
        } @else {
          <div class="mt-4">
            <p class="text-sm text-neutral-600" role="status">
              Cliente nuevo. Ingresa el nombre completo:
            </p>
            <label for="newName" class="sr-only">Nombre completo del cliente nuevo</label>
            <input
              id="newName"
              type="text"
              autocomplete="off"
              [formControl]="nameControl"
              placeholder="Nombre completo"
              class="mt-2 w-full rounded-md border border-neutral-300 bg-white px-3 py-2 text-neutral-900 shadow-sm focus:border-neutral-900 focus:ring-1 focus:ring-neutral-900 focus:outline-none"
            />
          </div>
        }
      }
    </section>
  `,
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
