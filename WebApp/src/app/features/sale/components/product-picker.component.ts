import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DecimalPipe } from '@angular/common';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-product-picker',
  imports: [ReactiveFormsModule, DecimalPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <section class="rounded-lg border border-neutral-200 bg-white p-5">
      <h2 class="text-sm font-semibold tracking-wide text-neutral-500 uppercase">Productos</h2>

      <form class="mt-4 flex flex-wrap items-end gap-3" [formGroup]="form" (submit)="onAdd($event)">
        <div class="flex-1 min-w-48">
          <label for="product" class="block text-sm font-medium text-neutral-700">Producto</label>
          <select
            id="product"
            formControlName="productId"
            class="mt-1 w-full rounded-md border border-neutral-300 bg-white px-3 py-2 text-neutral-900 shadow-sm focus:border-neutral-900 focus:ring-1 focus:ring-neutral-900 focus:outline-none"
          >
            <option value="" disabled>Selecciona un producto</option>
            @for (product of store.products(); track product.id) {
              <option [value]="product.id">{{ product.name }} — {{ product.unitCost | number: '1.2-2' }}</option>
            }
          </select>
        </div>
        <div class="w-24">
          <label for="quantity" class="block text-sm font-medium text-neutral-700">Cantidad</label>
          <input
            id="quantity"
            type="number"
            min="1"
            formControlName="quantity"
            class="mt-1 w-full rounded-md border border-neutral-300 bg-white px-3 py-2 text-neutral-900 shadow-sm focus:border-neutral-900 focus:ring-1 focus:ring-neutral-900 focus:outline-none"
          />
        </div>
        <button
          type="submit"
          [disabled]="form.invalid"
          class="rounded-md border border-neutral-900 px-4 py-2 text-sm font-medium text-neutral-900 transition hover:bg-neutral-900 hover:text-white disabled:cursor-not-allowed disabled:border-neutral-300 disabled:text-neutral-300 disabled:hover:bg-transparent"
        >
          Añadir
        </button>
      </form>
    </section>
  `,
})
export class ProductPickerComponent {
  protected readonly store = inject(SaleStore);

  protected readonly form = new FormGroup({
    productId: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    quantity: new FormControl(1, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
  });

  protected onAdd(event: Event): void {
    event.preventDefault();
    if (this.form.invalid) {
      return;
    }

    const { productId, quantity } = this.form.getRawValue();
    const product = this.store.products().find((p) => p.id === productId);
    if (!product) {
      return;
    }

    this.store.addItem(product, quantity);
    this.form.reset({ productId: '', quantity: 1 });
  }
}
