import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { LoadingService } from '../services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  private countRequest = 0;
  constructor(private loadingService: LoadingService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.countRequest) {
      this.loadingService.show();
    }

    this.countRequest++;

    return next.handle(request).pipe(
        finalize(() => {
            this.countRequest--;
            if (!this.countRequest) {
              this.loadingService.hide();
            }
        })
    );
  }
}

export const loadingInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
];
