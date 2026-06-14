import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { RegisterSaleRequest, SaleResult } from '../models/sale.model';

@Injectable({ providedIn: 'root' })
export class SalesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiBaseUrl}/sales`;

  /** Simulates the sale and returns the applied discount without persisting it. */
  calculateDiscount(request: RegisterSaleRequest): Observable<SaleResult> {
    return this.http.post<SaleResult>(`${this.baseUrl}/discount`, request);
  }

  /** Persists the complete sale, including the applied discounts. */
  register(request: RegisterSaleRequest): Observable<SaleResult> {
    return this.http.post<SaleResult>(this.baseUrl, request);
  }
}
