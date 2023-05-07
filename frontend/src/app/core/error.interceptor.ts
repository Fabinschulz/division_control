import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastService } from '../features/dividas/services/toast.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private toastrService: ToastService,
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((e: HttpErrorResponse) => {
        if (e.status === 400) {
          if (e?.error?.errors) {
            if (Array.isArray(e.error.errors)) {
              e.error.errors.forEach((error: string) => {
                this.toastError(e.error.errors, '');
              });
            } else {
              const errorEntries = Object.entries(e.error.errors);
              errorEntries.forEach((errorMessages: any[]) => {
                errorMessages[1].forEach((errorMessage: string) => {
                  this.toastError(e.error.errors, '');
                });
              });
            }
          }
          return throwError(() => e);
        }
        if (e.status === 500) {
          this.toastError(e.error.Message, '');
        }

        if (e.status === 404) {
          return throwError(() => e);
        }

        if (e.status === 0) {
          const url = e.url ? e.url.split('api/')[1] : '';
          this.toastError(
            `Erro em ${url}, por favor contate o administrador`,
            'Erro interno',
          );
          return throwError(() => e);
        }


        return throwError(() => e);
      })
    );
  }

  toastError(message: string, title = ''): void {
    this.toastrService.showErrorMessage(message, title);
  }

  toastWarning(message: string, title = ''): void {
    this.toastrService.showWarningMessage(message, title);
  }
}
