import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DecimalPipe } from '@angular/common';

import { SaleStore } from '../sale.store';

@Component({
  selector: 'app-product-picker',
  imports: [ReactiveFormsModule, DecimalPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './product-picker.component.html',
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
