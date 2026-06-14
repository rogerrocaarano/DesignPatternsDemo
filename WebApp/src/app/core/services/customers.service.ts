import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, catchError } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Customer } from '../models/customer.model';

@Injectable({ providedIn: 'root' })
export class CustomersService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiBaseUrl}/customers`;

  /** Returns the customer for the given NIT, or null when not found (404). */
  getByNit(nit: string): Observable<Customer | null> {
    return this.http.get<Customer>(`${this.baseUrl}/by-nit/${encodeURIComponent(nit)}`).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 404) {
          return of(null);
        }
        throw error;
      }),
    );
  }
}
