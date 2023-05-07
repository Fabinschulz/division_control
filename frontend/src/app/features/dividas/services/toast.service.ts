import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class ToastService {
  constructor(private toastr: ToastrService) {}

  showSuccessMessage(message: string, title: string) {
    this.toastr.success(message, title, {
      progressBar: true,
      timeOut: 6000,
      positionClass: 'toast-top-right'
    });

  }

  showErrorMessage(message: string, title: string) {
    this.toastr.error(message, title, {
      progressBar: true,
      timeOut: 6000,
    });
  }



  showWarningMessage(message: string, title: string) {
    this.toastr.warning(message, title, {
      progressBar: true,
      timeOut: 6000,
    });
  }
}
