import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/sale/sale-page.component').then((m) => m.SalePageComponent),
  },
  { path: '**', redirectTo: '' },
];
